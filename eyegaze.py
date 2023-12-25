import cv2
import mediapipe as mp
import numpy as np

FONTS = cv2.FONT_HERSHEY_COMPLEX

map_face_mesh = mp.solutions.face_mesh

RightEyeRight = [33]
RightEyeLeft = [133]
LeftEyeRight = [362]
LeftEyeLeft = [263]
LeftIris = [474, 475, 476, 477]
RightIris = [469, 470, 471, 472]

lmposition = []
with map_face_mesh.FaceMesh(max_num_faces=1, refine_landmarks=True, min_detection_confidence=0.5,
                            min_tracking_confidence=0.5) as face_mesh:
    cap = cv2.VideoCapture(0)
    flag, ctleft, ctright = 0, 0, 0
    prev_position = None  # Variable to store the previous position

    while True:
        ret, frame = cap.read()
        if not ret:
            break

        # resizing frame
        frame = cv2.flip(frame, 1)
        frame_height, frame_width = frame.shape[:2]
        rgb_frame = cv2.cvtColor(frame, cv2.COLOR_RGB2BGR)
        results = face_mesh.process(rgb_frame)
        h, w, c = frame.shape

        if results.multi_face_landmarks:
            facepoints = np.array([np.multiply([p.x, p.y], [w, h]).astype(int)
                                   for p in results.multi_face_landmarks[0].landmark])
            (cx, cy), radiusl = cv2.minEnclosingCircle(facepoints[LeftIris])
            (rx, ry), radiusr = cv2.minEnclosingCircle(facepoints[RightIris])
            centerright = np.array([rx, ry], dtype=np.int32)
            cv2.circle(frame, centerright, int(radiusl), (0, 0, 255), 1, cv2.LINE_AA)
            distanceHalf = np.linalg.norm(centerright - facepoints[RightEyeRight])
            distanceAll = np.linalg.norm(facepoints[RightEyeLeft] - facepoints[RightEyeRight])
            ratio = distanceHalf / distanceAll

            if ratio <= 0.4:
                position = 'The user is looking to the left'
                flag = 1
                ctright += 1
            elif 0.4 < ratio < 0.6:
                position = 'The user is focused in the game'
                flag = 1
            else:
                position = 'The user is looking to the Right'
                flag = 1
                ctleft += 1

            # Print only when the position changes
            if position != prev_position:
                print(position)
                prev_position = position

        cv2.imshow('frame', frame)
        key = cv2.waitKey(20)
        if key == ord('q') or key == ord('Q'):
            break

    cv2.destroyAllWindows()
    cap.release()
