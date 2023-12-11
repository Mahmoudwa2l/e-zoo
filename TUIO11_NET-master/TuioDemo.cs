/*
   TUIO C# Demo - part of the reacTIVision project
   Copyright (c) 2005-2016 Martin Kaltenbrunner <martin@tuio.org>

   This program is free software; you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation; either version 2 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program; if not, write to the Free Software
   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using TUIO;

// TuioDemo class inherits from Form and implements TuioListener interface
public class TuioDemo : Form, TuioListener
{
    // TuioClient for receiving TUIO events
    private TuioClient client;

    // Dictionaries to store Tuio objects, cursors, and blobs
    private Dictionary<long, TuioObject> objectList;
    private Dictionary<long, TuioCursor> cursorList;
    private Dictionary<long, TuioBlob> blobList;

    // Static variables for the window width and height
    public static int width, height;

<<<<<<< Updated upstream
    // Variables for window and screen dimensions
    private int window_width = 640;
    private int window_height = 480;
    private int window_left = 0;
    private int window_top = 0;
    private int screen_width = Screen.PrimaryScreen.Bounds.Width;
    private int screen_height = Screen.PrimaryScreen.Bounds.Height;

    // Boolean flags for fullscreen and verbosity
    private bool fullscreen;
    private bool verbose;
=======
		Font font = new Font("Arial", 10.0f);
		SolidBrush fntBrush = new SolidBrush(Color.White);
		SolidBrush bgrBrush = new SolidBrush(Color.FromArgb(0,0,64));
		SolidBrush curBrush = new SolidBrush(Color.FromArgb(192, 0, 192));
		SolidBrush objBrush = new SolidBrush(Color.FromArgb(64, 0, 0));
		SolidBrush blbBrush = new SolidBrush(Color.FromArgb(64, 64, 64));
		Pen curPen = new Pen(new SolidBrush(Color.Blue), 1);
		//private Image LoadImageForSymbolID(int symbolID)
		//{
		//	// Assuming the images are named "lion.png", "image_2.png", etc.
		//	string imageName = $"image_{symbolID}.png";
		//	string imagePath = Path.Combine("images", imageName);

		//	try
		//	{
		//		return Image.FromFile(imagePath);
		//	}
		//	catch (Exception ex)
		//	{
		//		// Handle exceptions, e.g., image not found
		//		Console.WriteLine($"Error loading image for SymbolID {symbolID}: {ex.Message}");
		//		return null; // or return a default image
		//	}
		//}



    public TuioDemo(int port) {
		
			verbose = false;
			fullscreen = false;
			width = window_width;
			height = window_height;
>>>>>>> Stashed changes

    // Fonts and brushes for rendering
    Font font = new Font("Arial", 10.0f);
    SolidBrush fntBrush = new SolidBrush(Color.Black);
    SolidBrush bgrBrush = new SolidBrush(Color.White);
    SolidBrush curBrush = new SolidBrush(Color.FromArgb(192, 0, 192));
    SolidBrush objBrush = new SolidBrush(Color.FromArgb(64, 0, 0));
    SolidBrush blbBrush = new SolidBrush(Color.FromArgb(64, 64, 64));
    Pen curPen = new Pen(new SolidBrush(Color.Blue), 1);

    // Constructor for TuioDemo class, takes a port as an argument
    public TuioDemo(int port)
    {

        // Initialize flags and dimensions
        verbose = false;
        fullscreen = false;
        width = window_width;
        height = window_height;

        // Set the size, name, and text of the form
        this.ClientSize = new System.Drawing.Size(width, height);
        this.Name = "E-Zoo";
        this.Text = "E-Zoo";

        // Event handlers for form closing and key down
        this.Closing += new CancelEventHandler(Form_Closing);
        this.KeyDown += new KeyEventHandler(Form_KeyDown);

        // Set drawing styles for better performance
        this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                      ControlStyles.UserPaint |
                      ControlStyles.DoubleBuffer, true);

        // Initialize dictionaries for Tuio objects, cursors, and blobs
        objectList = new Dictionary<long, TuioObject>(128);
        cursorList = new Dictionary<long, TuioCursor>(128);
        blobList = new Dictionary<long, TuioBlob>(128);

        // Initialize TuioClient and add this class as a listener
        client = new TuioClient(port);
        client.addTuioListener(this);

        // Connect to the TuioClient
        client.connect();
    }

    // Event handler for key down events
    private void Form_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        // Toggle fullscreen mode on F1 key press
        if (e.KeyData == Keys.F1)
        {
            if (fullscreen == false)
            {
                // Switch to fullscreen
                width = screen_width;
                height = screen_height;

                window_left = this.Left;
                window_top = this.Top;

                this.FormBorderStyle = FormBorderStyle.None;
                this.Left = 0;
                this.Top = 0;
                this.Width = screen_width;
                this.Height = screen_height;

                fullscreen = true;
            }
            else
            {
                // Switch to windowed mode
                width = window_width;
                height = window_height;

                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Left = window_left;
                this.Top = window_top;
                this.Width = window_width;
                this.Height = window_height;

                fullscreen = false;
            }
        }
        else if (e.KeyData == Keys.Escape)
        {
            // Close the application on Escape key press
            this.Close();
        }
        else if (e.KeyData == Keys.V)
        {
            // Toggle verbosity on V key press
            verbose = !verbose;
        }
    }

    // Event handler for form closing
    private void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        // Remove TuioListener and disconnect from TuioClient
        client.removeTuioListener(this);
        client.disconnect();
        // Exit the application
        System.Environment.Exit(0);
    }

    // Event handler for adding TuioObject
    public void addTuioObject(TuioObject o)
    {
        lock (objectList)
        {
            objectList.Add(o.SessionID, o);
        }
        if (verbose)
            Console.WriteLine("add obj " + o.SymbolID + " (" + o.SessionID + ") " + o.X + " " + o.Y + " " + o.Angle);
    }

    // Event handler for updating TuioObject
    public void updateTuioObject(TuioObject o)
    {
        if (verbose)
            Console.WriteLine("set obj " + o.SymbolID + " " + o.SessionID + " " + o.X + " " + o.Y + " " + o.Angle + " " + o.MotionSpeed + " " + o.RotationSpeed + " " + o.MotionAccel + " " + o.RotationAccel);
    }

    // Event handler for removing TuioObject
    public void removeTuioObject(TuioObject o)
    {
        lock (objectList)
        {
            objectList.Remove(o.SessionID);
        }
        if (verbose)
            Console.WriteLine("del obj " + o.SymbolID + " (" + o.SessionID + ")");
    }

    // Event handler for adding TuioCursor
    public void addTuioCursor(TuioCursor c)
    {
        lock (cursorList)
        {
            cursorList.Add(c.SessionID, c);
        }
        if (verbose)
            Console.WriteLine("add cur " + c.CursorID + " (" + c.SessionID + ") " + c.X + " " + c.Y);
    }

    // Event handler for updating TuioCursor
    public void updateTuioCursor(TuioCursor c)
    {
        if (verbose)
            Console.WriteLine("set cur " + c.CursorID + " (" + c.SessionID + ") " + c.X + " " + c.Y + " " + c.MotionSpeed + " " + c.MotionAccel);
    }

    // Event handler for removing TuioCursor
    public void removeTuioCursor(TuioCursor c)
    {
        lock (cursorList)
        {
            cursorList.Remove(c.SessionID);
        }
        if (verbose)
            Console.WriteLine("del cur " + c.CursorID + " (" + c.SessionID + ")");
    }

    // Event handler for adding TuioBlob
    public void addTuioBlob(TuioBlob b)
    {
        lock (blobList)
        {
            blobList.Add(b.SessionID, b);
        }
        if (verbose)
            Console.WriteLine("add blb " + b.BlobID + " (" + b.SessionID + ") " + b.X + " " + b.Y + " " + b.Angle + " " + b.Width + " " + b.Height + " " + b.Area);
    }

    // Event handler for updating TuioBlob
    public void updateTuioBlob(TuioBlob b)
    {
        if (verbose)
            Console.WriteLine("set blb " + b.BlobID + " (" + b.SessionID + ") " + b.X + " " + b.Y + " " + b.Angle + " " + b.Width + " " + b.Height + " " + b.Area + " " + b.MotionSpeed + " " + b.RotationSpeed + " " + b.MotionAccel + " " + b.RotationAccel);
    }

    // Event handler for removing TuioBlob
    public void removeTuioBlob(TuioBlob b)
    {
        lock (blobList)
        {
            blobList.Remove(b.SessionID);
        }
        if (verbose)
            Console.WriteLine("del blb " + b.BlobID + " (" + b.SessionID + ")");
    }

    // Event handler for refreshing the display
    public void refresh(TuioTime frameTime)
    {
        // Invalidate the form to trigger a repaint
        Invalidate();
    }

    // Event handler for painting the background
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        // Getting the graphics object
        Graphics g = pevent.Graphics;
        // Fill the background with white
        g.FillRectangle(bgrBrush, new Rectangle(0, 0, width, height));

        // Draw the cursor path
        if (cursorList.Count > 0)
        {
            lock (cursorList)
            {
                foreach (TuioCursor tcur in cursorList.Values)
                {
                    List<TuioPoint> path = tcur.Path;
                    TuioPoint current_point = path[0];

                    for (int i = 0; i < path.Count; i++)
                    {
                        TuioPoint next_point = path[i];
                        g.DrawLine(curPen, current_point.getScreenX(width), current_point.getScreenY(height), next_point.getScreenX(width), next_point.getScreenY(height));
                        current_point = next_point;
                    }
                    g.FillEllipse(curBrush, current_point.getScreenX(width) - height / 100, current_point.getScreenY(height) - height / 100, height / 50, height / 50);
                    g.DrawString(tcur.CursorID + "", font, fntBrush, new PointF(tcur.getScreenX(width) - 10, tcur.getScreenY(height) - 10));
                }
            }
        }

        // Draw the objects
        if (objectList.Count > 0)
        {
            lock (objectList)
            {
                foreach (TuioObject tobj in objectList.Values)
                {
                    int ox = tobj.getScreenX(width);
                    int oy = tobj.getScreenY(height);
                    int size = height / 10;

                    g.TranslateTransform(ox, oy);
                    g.RotateTransform((float)(tobj.Angle / Math.PI * 180.0f));
                    g.TranslateTransform(-ox, -oy);

<<<<<<< Updated upstream
                    g.FillRectangle(objBrush, new Rectangle(ox - size / 2, oy - size / 2, size, size));

                    g.TranslateTransform(ox, oy);
                    g.RotateTransform(-1 * (float)(tobj.Angle / Math.PI * 180.0f));
                    g.TranslateTransform(-ox, -oy);

                    g.DrawString(tobj.SymbolID + "", font, fntBrush, new PointF(ox - 10, oy - 10));
                }
            }
        }

        // Draw the blobs
        if (blobList.Count > 0)
        {
            lock (blobList)77
            {
                foreach (TuioBlob tblb in blobList.Values)
                {
                    int bx = tblb.getScreenX(width);
                    int by = tblb.getScreenY(height);
                    float bw = tblb.Width * width;
                    float bh = tblb.Height * height;

                    g.TranslateTransform(bx, by);
                    g.RotateTransform((float)(tblb.Angle / Math.PI * 180.0f));
                    g.TranslateTransform(-bx, -by);

                    g.FillEllipse(blbBrush, bx - bw / 2, by - bh / 2, bw, bh);
=======
		draw the objects

		if (objectList.Count > 0)
		{
			lock (objectList)
			{
				foreach (TuioObject tobj in objectList.Values)
				{
					int ox = tobj.getScreenX(width);
					int oy = tobj.getScreenY(height);
					int size = height / 10;

					g.TranslateTransform(ox, oy);
					g.RotateTransform((float)(tobj.Angle / Math.PI * 180.0f));
					g.TranslateTransform(-ox, -oy);

					g.FillRectangle(objBrush, new Rectangle(ox - size / 2, oy - size / 2, size, size));

					g.TranslateTransform(ox, oy);
					g.RotateTransform(-1 * (float)(tobj.Angle / Math.PI * 180.0f));
					g.TranslateTransform(-ox, -oy);

					g.DrawString(tobj.SymbolID + "", font, fntBrush, new PointF(ox - 10, oy - 10));
				}
			}
		}

		draw the objects
		draw the objects

		//if (objectList.Count > 0)
  //      {
  //          lock (objectList)
  //          {
  //              foreach (TuioObject tobj in objectList.Values)
  //              {
  //                  int ox = tobj.getScreenX(width);
  //                  int oy = tobj.getScreenY(height);
  //                  int size = height / 10;

  //                  g.TranslateTransform(ox, oy);
  //                  g.RotateTransform((float)(tobj.Angle / Math.PI * 180.0f));
  //                  g.TranslateTransform(-ox, -oy);

  //                  // Load the image based on SymbolID
  //                  Image objImage = LoadImageForSymbolID(tobj.SymbolID);

  //                  // Draw the image instead of a rectangle
  //                  g.DrawImage(objImage, new Rectangle(ox - size / 2, oy - size / 2, size, size));

  //                  g.TranslateTransform(ox, oy);
  //                  g.RotateTransform(-1 * (float)(tobj.Angle / Math.PI * 180.0f));
  //                  g.TranslateTransform(-ox, -oy);

  //                  g.DrawString(tobj.SymbolID + "", font, fntBrush, new PointF(ox - 10, oy - 10));
  //              }
  //          }
  //      }



        // draw the blobs
        if (blobList.Count > 0) {
				lock(blobList) {
					foreach (TuioBlob tblb in blobList.Values) {
						int bx = tblb.getScreenX(width);
						int by = tblb.getScreenY(height);
						float bw = tblb.Width*width;
						float bh = tblb.Height*height;
>>>>>>> Stashed changes

                    g.TranslateTransform(bx, by);
                    g.RotateTransform(-1 * (float)(tblb.Angle / Math.PI * 180.0f));
                    g.TranslateTransform(-bx, -by);

                    g.DrawString(tblb.BlobID + "", font, fntBrush, new PointF(bx, by));
                }
            }
        }
    }

    // Main method to start the application
    public static void Main(String[] argv)
    {
        int port = 0;
        switch (argv.Length)
        {
            case 1:
                port = int.Parse(argv[0], null);
                if (port == 0) goto default;
                break;
            case 0:
                port = 3333;
                break;
            default:
                Console.WriteLine("usage: mono TuioDemo [port]");
                System.Environment.Exit(0);
                break;
        }

        // Create an instance of TuioDemo and run the application
        TuioDemo app = new TuioDemo(port);
        Application.Run(app);
    }
}
