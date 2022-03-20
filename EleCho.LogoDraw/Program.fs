open System
open System.Drawing
open System.Drawing.Drawing2D
    
let draw (size:int) (color:Color) : Bitmap =
    let sizeF = float size
    let sizeRF = sizeF / 2.0
    
    let canvas = new Bitmap (size, size)
    let g = Graphics.FromImage (canvas)
    let white = Brushes.White
    let brush = new SolidBrush(color)

    g.SmoothingMode <- SmoothingMode.AntiAlias
    g.Clear(Color.White)
    
    let centerCircleInL = 0.533 * sizeF
    let centerCircleInR = centerCircleInL / 2.0
    let centerCircleInPos = (sizeF - centerCircleInL) / 2.0
    let centerCircleInRect = RectangleF (float32 centerCircleInPos, float32 centerCircleInPos, float32 centerCircleInL, float32 centerCircleInL)
    let centerCircleOutL = 0.700 * sizeF
    let centerCircleOutR = centerCircleOutL / 2.0
    let centerCircleOutPos = (sizeF - centerCircleOutL) / 2.0
    let centerCircleOutRect = RectangleF (float32 centerCircleOutPos, float32 centerCircleOutPos, float32 centerCircleOutL, float32 centerCircleOutL)
    g.FillEllipse (brush, centerCircleOutRect)
    g.FillEllipse (white, centerCircleInRect)
    
    let centerPointL = 0.333 * sizeF
    let centerPointPos = (sizeF - centerPointL) / 2.0
    let centerPointRect = RectangleF(float32 centerPointPos, float32 centerPointPos, float32 centerPointL, float32 centerPointL)
    g.FillEllipse (brush, centerPointRect)
    
    let roundPointR = 0.345 * sizeF
    let roundPointInR = 0.078 * sizeF
    let roundPointInL = 2.0 * roundPointInR
    let roundPointOutR = 0.128 * sizeF
    let roundPointOutL = 2.0 * roundPointOutR
    let roundPointInSize = SizeF(float32 roundPointInL, float32 roundPointInL)
    let roundPointOutSize = SizeF (float32 roundPointOutL, float32 roundPointOutL)
    let roundPoints = [ for i in [Math.PI / 3.0; Math.PI; Math.PI / -3.0] do 
                            PointF (float32 (roundPointR * Math.Cos(i) + sizeRF), float32 (roundPointR * Math.Sin(i) + sizeRF)) ]
    let roundPointInPoss = [ for i in roundPoints do PointF(i.X - float32 roundPointInR, i.Y - float32 roundPointInR) ]
    let roundPointInRects = [ for i in roundPointInPoss do RectangleF (i, roundPointInSize) ]
    let roundPointOutPoss = [ for i in roundPoints do PointF(i.X - float32 roundPointOutR, i.Y - float32 roundPointOutR) ]
    let roundPointOutRects = [ for i in roundPointOutPoss do RectangleF (i, roundPointOutSize) ]
    
    for i in roundPointOutRects do
        g.FillEllipse (white, i)
    for i in roundPointInRects do
        g.FillEllipse (brush, i)
                            
    canvas

[<EntryPoint>]
let main argv =
    let bmp = draw 1000 (Color.FromArgb (0, 120, 212))
    bmp.Save("elecho.jpg")

    0
    