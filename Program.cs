using System;
using OpenCvSharp.Tracking;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Target_Traker
{
    class Program
    {
        static void Main(string[] args)
        {
            

            var capture = new VideoCapture(@"G:\videodata\char 5s.mp4");
            var win = new Window("capture");
            var mat = new Mat();
            var tracker = TrackerCSRT.Create();
            var bbox = new Rect();
            var bboxes = new Rect[] { };
            
            capture.Read(mat);

            bbox = Cv2.SelectROI("capture", mat);
            //bboxes = Cv2.SelectROIs("capture", mat);

            //for(int i = 0; i < bboxes.Length; i++)
            //{
            //    Console.WriteLine(bboxes[i]);
            //}

            //var mt = MultiTracker.Create();
            Console.WriteLine(bbox);

            var bbox2d = new Rect2d(bbox.X,bbox.Y,bbox.Width,bbox.Height);


            //rect2d to rect

            tracker.Init(mat, bbox2d);
            


            while (true)
            {
                capture.Read(mat);
                // 読み込めるフレームがなくなったら終了
                if (mat.Empty()) { break; }

                tracker.Update(mat, ref bbox2d);

                Cv2.Rectangle(mat, bbox2d.ToRect(), new Scalar(0, 255, 0));

                //for (int i = 0; i < bboxes.Length; i++)
                //{
                //    Cv2.Rectangle(mat, bbox2d.ToRect(), new Scalar(0, 255, 0));
                //}

                Console.WriteLine(bbox2d);

                win.ShowImage(mat);



                Cv2.WaitKey(33);

            }

            Console.ReadKey();


        }
    }


}
