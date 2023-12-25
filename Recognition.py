import cv2
import os
import glob
import numpy as np
import face_recognition

known_face = []
known_name = []
def load_images(path):
        path = glob.glob(os.path.join(path, "*.*"))
        print("{} images found.".format(len(path)))
        for paths in path:
            img = cv2.imread(paths)
            rgb_img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
            basename = os.path.basename(paths)
            (file, ext) = os.path.splitext(basename)
            img_encoding = face_recognition.face_encodings(rgb_img)[0]

            known_face.append(img_encoding)
            known_name.append(file)
        print("loaded encoded images")


def detect(frame):
        small = cv2.resize(frame, (0, 0), fx=0.25, fy=0.25)
        rgb = cv2.cvtColor(small, cv2.COLOR_BGR2RGB)
        location = face_recognition.face_locations(rgb)
        encode = face_recognition.face_encodings(rgb, location)

        face_names = []
        for encodes in encode:
            matches = face_recognition.compare_faces(known_face, encodes)
            name = "Unknown"
            distance = face_recognition.face_distance(known_face, encodes)
            best = np.argmin(distance)
            if matches[best]:
                name = known_name [best]
            face_names.append(name)

        location = np.array(location)
        location = location / 0.25
        return location.astype(int), face_names


facerec = load_images('images/')
cap = cv2.VideoCapture(0)
while True:
    ret, frame = cap.read()
    face_locations, face_names = detect(frame)
    for face_loc, name in zip(face_locations, face_names):
        y1, x2, y2, x1 = face_loc[0], face_loc[1], face_loc[2], face_loc[3]

        cv2.putText(frame, name,(x1, y1 - 10), cv2.FONT_HERSHEY_DUPLEX, 1, (0, 0, 200), 2)
        cv2.rectangle(frame, (x1, y1), (x2, y2), (0, 0, 200), 4)

    cv2.imshow("Frame", frame)

    key = cv2.waitKey(1)
    if key == 27:
        break