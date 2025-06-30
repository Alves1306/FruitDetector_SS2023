using System;
using FruitDLL;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Emgu.CV.CvEnum;
using Emgu.CV.Flann;

namespace SS_OpenCV
{
    class ImageClass
    {
        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        public static void Negative(Image<Bgr, byte> img)
        {

            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //retrieve 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];


                        // store in the image
                        dataPtr[0] = (byte)(255 - blue);
                        dataPtr[1] = (byte)(255 - green);
                        dataPtr[2] = (byte)(255 - red);

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
            }
        }

        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        public static void NegativeSlow(Image<Bgr, byte> img)
        {
            int x, y;

            Bgr aux;
            for (y = 0; y < img.Height; y++)
            {
                for (x = 0; x < img.Width; x++)
                {
                    // acesso pela biblioteca : mais lento 
                    aux = img[y, x];
                    img[y, x] = new Bgr(255 - aux.Blue, 255 - aux.Green, 255 - aux.Red);
                }
            }
        }

        /// <summary>
        /// Convert to gray
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        /// <summary>
        /// RedChannel
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = dataPtr[0];//tenho que por em inteiro contraste* e brilho+
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // store in the image
                            dataPtr[0] = red;
                            dataPtr[1] = red;
                            dataPtr[2] = red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }
        public static void BlueChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = dataPtr[0];//tenho que por em inteiro contraste* e brilho+
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // store in the image
                            dataPtr[0] = blue;
                            dataPtr[1] = blue;
                            dataPtr[2] = blue;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }
        //BrightContrast
        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;

                // é a melhor forma de obter os valores do utilizador?
                InputBox bright2 = new InputBox("Invalide value! Try again?(0-255)");
                InputBox bright1 = new InputBox("bright?(0)");
                bright1.ShowDialog();
                int brilho = Convert.ToInt32(bright1.ValueTextBox.Text);
                while (brilho > 255 || brilho < (-255))
                {
                    bright2.ShowDialog();
                    brilho = Convert.ToInt32(bright2.ValueTextBox.Text);
                }

                InputBox contrast2 = new InputBox("Invalide value! Try again?(0-3)");
                InputBox contrast1 = new InputBox("contrast?(1)");
                contrast1.ShowDialog();
                float contraste = Convert.ToSingle(contrast1.ValueTextBox.Text); //n consigo por decimais
                while (contraste > 3 || contraste < 1)
                {
                    contrast2.ShowDialog();
                    contraste = Convert.ToSingle(contrast2.ValueTextBox.Text);
                }


                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //por que n funciona?
                            //retrieve 3 colour components
                            blue = dataPtr[0];//tenho que por em inteiro contraste* e brilho+
                            green = dataPtr[1];
                            red = dataPtr[2];

                            //contrast and bright
                            blue = (byte)Math.Round((blue * contraste + brilho));
                            if (blue > 255)
                                blue = 255;
                            if (blue < 0)
                                blue = 0;

                            green = (byte)Math.Round((green * contraste + brilho));
                            if (green > 255)
                                green = 255;
                            if (green < 0)
                                green = 0;

                            red = (byte)Math.Round((red * contraste + brilho));
                            if (red > 255)
                                red = 255;
                            if (red < 0)
                                red = 0;

                            // store in the image
                            dataPtr[0] = blue;
                            dataPtr[1] = green;
                            dataPtr[2] = red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }

                }

            }
        }
        //traslation
        public static void Translation(Image<Bgr, byte> imgDestino, Image<Bgr, byte> imgOrigem, int dx, int dy)
        {
            unsafe
            {
                MIplImage mD = imgDestino.MIplImage; // Pointer to the imageOrigem
                byte* dataPtrDestino = (byte*)mD.ImageData.ToPointer(); // Pointer to the imageOrigem

                MIplImage mO = imgOrigem.MIplImage;
                byte* dataPtrOrigem = (byte*)mO.ImageData.ToPointer(); // Pointer to the imageOrigem
                byte blue, green, red;

                int width = imgOrigem.Width;
                int height = imgOrigem.Height;
                int width2 = imgOrigem.Width + dx;
                int height2 = imgOrigem.Height + dy;
                int nChan = mD.NChannels; // number of channels - 3
                int padding = mD.WidthStep - mD.NChannels * mD.Width; // alinhament bytes (padding)
                int widthstep = mD.WidthStep;
                int x, y, ya = 0 - dy, xa = 0 - dx;


                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if (y >= height2 || x >= width2)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else
                            {
                                blue = (byte)(dataPtrOrigem + ya * widthstep + xa * nChan)[0];
                                green = (byte)(dataPtrOrigem + ya * widthstep + xa * nChan)[1];
                                red = (byte)(dataPtrOrigem + ya * widthstep + xa * nChan)[2];
                            }

                            // store in the image
                            dataPtrDestino[0] = blue;
                            dataPtrDestino[1] = green;
                            dataPtrDestino[2] = red;

                            // advance the pointer to the next pixel
                            dataPtrDestino += nChan; xa++;
                        }
                        xa = 0;

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtrDestino += padding; ya++;
                    }

                }

            }
        }
        public static void Scale_point_xy(Image<Bgr, byte> imgDestino, Image<Bgr, byte> imgOrigem, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                MIplImage mD = imgDestino.MIplImage; // Pointer to the imageOrigem
                byte* dataPtrDestino = (byte*)mD.ImageData.ToPointer(); // Pointer to the imageOrigem

                MIplImage mO = imgOrigem.MIplImage;
                byte* dataPtrOrigem = (byte*)mO.ImageData.ToPointer(); // Pointer to the imageOrigem

                byte blue, green, red;
                int width = imgOrigem.Width;
                int height = imgOrigem.Height;

                int nChan = mD.NChannels; // number of channels - 3
                int padding = mD.WidthStep - mD.NChannels * mD.Width; // alinhament bytes (padding)
                int widthstep = mD.WidthStep;

                dataPtrOrigem += centerX * nChan + centerY * widthstep;
                int x, y, ya, xa;
                int widthstepaux, nChanaux;

                //centerX----centerY---------scaleFactor(float)-------

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        ya = (int)Math.Round((y - centerY) / scaleFactor);
                        widthstepaux = ya * widthstep;

                        for (x = 0; x < width; x++)
                        {
                            xa = (int)Math.Round((x - centerX) / scaleFactor);
                            nChanaux = xa * nChan;

                            blue = (byte)(dataPtrOrigem + widthstepaux + nChanaux)[0];
                            green = (byte)(dataPtrOrigem + widthstepaux + nChanaux)[1];
                            red = (byte)(dataPtrOrigem + widthstepaux + nChanaux)[2];

                            // store in the image
                            dataPtrDestino[0] = blue;
                            dataPtrDestino[1] = green;
                            dataPtrDestino[2] = red;

                            // advance the pointer to the next pixel
                            dataPtrDestino += nChan;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtrDestino += padding;
                    }

                }

            }
        }
        public static void Scale(Image<Bgr, byte> imgDestino, Image<Bgr, byte> imgOrigem, float scaleFactor)
        {
            unsafe
            {
                MIplImage mD = imgDestino.MIplImage; // Pointer to the imageOrigem
                byte* dataPtrDestino = (byte*)mD.ImageData.ToPointer(); // Pointer to the imageOrigem

                MIplImage mO = imgOrigem.MIplImage;
                byte* dataPtrOrigem = (byte*)mO.ImageData.ToPointer(); // Pointer to the imageOrigem

                byte blue, green, red;
                int width = imgOrigem.Width;
                int height = imgOrigem.Height;

                int nChan = mD.NChannels; // number of channels - 3
                int padding = mD.WidthStep - mD.NChannels * mD.Width; // alinhament bytes (padding)
                int widthstep = mD.WidthStep;
                int x, y, ya, xa;
                int widthstepaux, nChanaux;

                //centerX----centerY---------scaleFactor(float)-------

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        ya = (int)Math.Round((y) / scaleFactor);
                        widthstepaux = ya * widthstep;

                        for (x = 0; x < width; x++)
                        {
                            xa = (int)Math.Round((x) / scaleFactor);
                            nChanaux = xa * nChan;

                            if (ya > height || xa > width)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else
                            {
                                blue = (byte)(dataPtrOrigem + widthstepaux + nChanaux)[0];
                                green = (byte)(dataPtrOrigem + widthstepaux + nChanaux)[1];
                                red = (byte)(dataPtrOrigem + widthstepaux + nChanaux)[2];
                            }
                            // store in the image
                            dataPtrDestino[0] = blue;
                            dataPtrDestino[1] = green;
                            dataPtrDestino[2] = red;

                            // advance the pointer to the next pixel
                            dataPtrDestino += nChan;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtrDestino += padding;
                    }

                }

            }
        }
        public static void Rotation(Image<Bgr, byte> imgDestino, Image<Bgr, byte> imgOrigem, float angle)
        {
            unsafe
            {
                MIplImage imgD = imgDestino.MIplImage;
                byte* imgDPoiter = (byte*)imgD.ImageData.ToPointer(); // Pointer to the image

                MIplImage imgO = imgOrigem.MIplImage;
                byte* imgOPoiter = (byte*)imgO.ImageData.ToPointer(); // Pointer to the image


                int width = imgO.Width;
                int height = imgO.Height;
                int nChan = imgO.NChannels; // number of channels - 3
                int padding = imgO.WidthStep - imgO.NChannels * imgO.Width; // alinhament bytes (padding)
                int widthstep = imgO.WidthStep;
                byte blue, green, red;
                int x, y;
                int xr, yr;   //y e x responsaveis (da imagem origem)
                double cos_angle = Math.Cos(angle);
                double sin_angle = Math.Sin(angle);
                double w2 = width / 2.0;
                double h2 = height / 2.0;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        { //(0;0)->destino
                          //calculo das cordenadas da origem

                            xr = (int)Math.Round((x - w2) * cos_angle - (h2 - y) * sin_angle + w2);
                            yr = (int)Math.Round(h2 - (x - w2) * sin_angle - (h2 - y) * cos_angle);
                            // getting color from imagO
                            if (yr < 0 || yr >= height || xr < 0 || xr >= width)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else
                            {
                                blue = (byte)(imgOPoiter + yr * widthstep + xr * nChan)[0];
                                green = (byte)(imgOPoiter + yr * widthstep + xr * nChan)[1];
                                red = (byte)(imgOPoiter + yr * widthstep + xr * nChan)[2];
                            }

                            // setting final image
                            imgDPoiter[0] = blue;
                            imgDPoiter[1] = green;
                            imgDPoiter[2] = red;

                            imgDPoiter += nChan;
                        }
                        imgDPoiter += padding;
                    }

                }
            }
        }
        public static void Mean(Image<Bgr, byte> imgDest, Image<Bgr, byte> imgOrig)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = imgOrig.MIplImage;
                MIplImage m1 = imgDest.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                byte* dataPtr1 = (byte*)m1.ImageData.ToPointer(); // Pointer to the image
                float bluetotal, greentotal, redtotal;
                byte blue, green, red;

                int width = imgDest.Width;
                int height = imgDest.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    //primeiro pixel de cima
                    bluetotal = 4 * (dataPtr)[0];
                    bluetotal += 2 * (dataPtr + 3)[0];
                    bluetotal += 2 * (dataPtr + m.WidthStep)[0];
                    bluetotal += (dataPtr + m.WidthStep + 3)[0];
                    blue = (byte)(bluetotal / 9);
                    redtotal = 4 * (dataPtr)[2];
                    redtotal += 2 * (dataPtr + 3)[2];
                    redtotal += 2 * (dataPtr + m.WidthStep)[2];
                    redtotal += (dataPtr + m.WidthStep + 3)[2];
                    red = (byte)(redtotal / 9);
                    greentotal = 4 * (dataPtr)[1];
                    greentotal += 2 * (dataPtr + 3)[1];
                    greentotal += 2 * (dataPtr + m.WidthStep)[1];
                    greentotal += (dataPtr + m.WidthStep + 3)[1];
                    green = (byte)(greentotal / 9);
                    dataPtr1[0] = blue;
                    dataPtr1[1] = green;
                    dataPtr1[2] = red;
                    dataPtr += nChan;
                    dataPtr1 += nChan;

                    //linha de cima
                    for (x = 1; x < width - 1; x++)
                    {
                        bluetotal = 2 * (dataPtr)[0];
                        bluetotal += 2 * (dataPtr - 3)[0];
                        bluetotal += 2 * (dataPtr + 3)[0];
                        bluetotal += (dataPtr + m.WidthStep + 3)[0];
                        bluetotal += (dataPtr + m.WidthStep)[0];
                        bluetotal += (dataPtr + m.WidthStep - 3)[0];
                        blue = (byte)(bluetotal / 9);
                        greentotal = 2 * (dataPtr)[1];
                        greentotal += 2 * (dataPtr - 3)[1];
                        greentotal += 2 * (dataPtr + 3)[1];
                        greentotal += (dataPtr + m.WidthStep + 3)[1];
                        greentotal += (dataPtr + m.WidthStep)[1];
                        greentotal += (dataPtr + m.WidthStep - 3)[1];
                        green = (byte)(greentotal / 9);
                        redtotal = 2 * (dataPtr)[2];
                        redtotal += 2 * (dataPtr - 3)[2];
                        redtotal += 2 * (dataPtr + 3)[2];
                        redtotal += (dataPtr + m.WidthStep + 3)[2];
                        redtotal += (dataPtr + m.WidthStep)[2];
                        redtotal += (dataPtr + m.WidthStep - 3)[2];
                        red = (byte)(redtotal / 9);
                        dataPtr1[0] = blue;
                        dataPtr1[1] = green;
                        dataPtr1[2] = red;
                        dataPtr += nChan;
                        dataPtr1 += nChan;
                    }

                    //ultimo pixel de cima
                    bluetotal = 4 * (dataPtr)[0];
                    bluetotal += 2 * (dataPtr - 3)[0];
                    bluetotal += 2 * (dataPtr + m.WidthStep)[0];
                    bluetotal += (dataPtr + m.WidthStep - 3)[0];
                    blue = (byte)(bluetotal / 9);
                    redtotal = 4 * (dataPtr)[2];
                    redtotal += 2 * (dataPtr + 3)[2];
                    redtotal += 2 * (dataPtr + m.WidthStep)[2];
                    redtotal += (dataPtr + m.WidthStep - 3)[2];
                    red = (byte)(redtotal / 9);
                    greentotal = 4 * (dataPtr)[1];
                    greentotal += 2 * (dataPtr + 3)[1];
                    greentotal += 2 * (dataPtr + m.WidthStep)[1];
                    greentotal += (dataPtr + m.WidthStep - 3)[1];
                    green = (byte)(greentotal / 9);
                    dataPtr1[0] = blue;
                    dataPtr1[1] = green;
                    dataPtr1[2] = red;
                    dataPtr += padding;
                    dataPtr1 += padding;

                    // coluna da direita
                    for (y = 1; y < height - 1; y++)
                    {
                        bluetotal = 2 * (dataPtr)[0];
                        bluetotal += 2 * (dataPtr - m.WidthStep)[0];
                        bluetotal += 2 * (dataPtr + m.WidthStep)[0];
                        bluetotal += (dataPtr + m.WidthStep - 3)[0];
                        bluetotal += (dataPtr - m.WidthStep - 3)[0];
                        bluetotal += (dataPtr - 3)[0];
                        blue = (byte)(bluetotal / 9);
                        greentotal = 2 * (dataPtr)[1];
                        greentotal += 2 * (dataPtr - m.WidthStep)[1];
                        greentotal += 2 * (dataPtr + m.WidthStep)[1];
                        greentotal += (dataPtr + m.WidthStep - 3)[1];
                        greentotal += (dataPtr - m.WidthStep - 3)[1];
                        greentotal += (dataPtr - 3)[1];
                        green = (byte)(greentotal / 9);
                        redtotal = 2 * (dataPtr)[2];
                        redtotal += 2 * (dataPtr - m.WidthStep)[2];
                        redtotal += 2 * (dataPtr + m.WidthStep)[2];
                        redtotal += (dataPtr + m.WidthStep - 3)[2];
                        redtotal += (dataPtr - m.WidthStep - 3)[2];
                        redtotal += (dataPtr - 3)[2];
                        red = (byte)(redtotal / 9);
                        dataPtr1[0] = blue;
                        dataPtr1[1] = green;
                        dataPtr1[2] = red;
                        dataPtr += padding;
                        dataPtr1 += padding;
                    }
                    //ultimo pixel de baixo
                    bluetotal = 4 * (dataPtr)[0];
                    bluetotal += 2 * (dataPtr - 3)[0];
                    bluetotal += 2 * (dataPtr - m.WidthStep)[0];
                    bluetotal += (dataPtr - m.WidthStep - 3)[0];
                    blue = (byte)(bluetotal / 9);
                    greentotal = 4 * (dataPtr)[1];
                    greentotal += 2 * (dataPtr - 3)[1];
                    greentotal += 2 * (dataPtr - m.WidthStep)[1];
                    greentotal += (dataPtr - m.WidthStep - 3)[1];
                    green = (byte)(greentotal / 9);
                    redtotal = 4 * (dataPtr)[2];
                    redtotal += 2 * (dataPtr - 3)[2];
                    redtotal += 2 * (dataPtr - m.WidthStep)[2];
                    redtotal += (dataPtr - m.WidthStep - 3)[2];
                    red = (byte)(redtotal / 9);
                    dataPtr1[0] = blue;
                    dataPtr1[1] = green;
                    dataPtr1[2] = red;
                    dataPtr -= padding;
                    dataPtr1 -= padding;

                    //linha de baixo
                    for (x = width - 1; x > 1; x--)
                    {
                        bluetotal = 2 * (dataPtr)[0];
                        bluetotal += 2 * (dataPtr - 3)[0];
                        bluetotal += 2 * (dataPtr + 3)[0];
                        bluetotal += (dataPtr - m.WidthStep + 3)[0];
                        bluetotal += (dataPtr - m.WidthStep)[0];
                        bluetotal += (dataPtr - m.WidthStep - 3)[0];
                        blue = (byte)(bluetotal / 9);
                        greentotal = 2 * (dataPtr)[1];
                        greentotal += 2 * (dataPtr - 3)[1];
                        greentotal += 2 * (dataPtr + 3)[1];
                        greentotal += (dataPtr - m.WidthStep + 3)[1];
                        greentotal += (dataPtr - m.WidthStep)[1];
                        greentotal += (dataPtr - m.WidthStep - 3)[1];
                        green = (byte)(greentotal / 9);
                        redtotal += 2 * (dataPtr - 3)[2];
                        redtotal += 2 * (dataPtr + 3)[2];
                        redtotal += (dataPtr - m.WidthStep + 3)[2];
                        redtotal += (dataPtr - m.WidthStep)[2];
                        redtotal += (dataPtr - m.WidthStep - 3)[2];
                        red = (byte)(redtotal / 9);
                        dataPtr1[0] = blue;
                        dataPtr1[1] = green;
                        dataPtr1[2] = red;
                        dataPtr -= nChan;
                        dataPtr1 -= nChan;
                    }

                    //primeirao de baixo
                    bluetotal = 4 * (dataPtr)[0];
                    bluetotal += 2 * (dataPtr - m.WidthStep)[0];
                    bluetotal += 2 * (dataPtr + 3)[0];
                    bluetotal += (dataPtr - m.WidthStep + 3)[0];
                    blue = (byte)(bluetotal / 9);
                    greentotal = 4 * (dataPtr)[1];
                    greentotal += 2 * (dataPtr - m.WidthStep)[1];
                    greentotal += 2 * (dataPtr + 3)[1];
                    greentotal += (dataPtr - m.WidthStep + 3)[1];
                    green = (byte)(greentotal / 9);
                    redtotal = 4 * (dataPtr)[2];
                    redtotal += 2 * (dataPtr - m.WidthStep)[2];
                    redtotal += 2 * (dataPtr + 3)[2];
                    redtotal += (dataPtr - m.WidthStep + 3)[2];
                    red = (byte)(redtotal / 9);
                    dataPtr1[0] = blue;
                    dataPtr1[1] = green;
                    dataPtr1[2] = red;
                    dataPtr += nChan;
                    dataPtr1 += nChan;

                    // coluna da esquerda
                    for (y = height - 1; y > 1; y--)
                    {
                        bluetotal = 2 * (dataPtr)[0];
                        bluetotal += 2 * (dataPtr - m.WidthStep)[0];
                        bluetotal += 2 * (dataPtr + m.WidthStep)[0];
                        bluetotal += (dataPtr + m.WidthStep + 3)[0];
                        bluetotal += (dataPtr - m.WidthStep + 3)[0];
                        bluetotal += (dataPtr + 3)[0];
                        blue = (byte)(bluetotal / 9);
                        greentotal = 2 * (dataPtr)[1];
                        greentotal += 2 * (dataPtr - m.WidthStep)[1];
                        greentotal += 2 * (dataPtr + m.WidthStep)[1];
                        greentotal += (dataPtr + m.WidthStep + 3)[1];
                        greentotal += (dataPtr - m.WidthStep + 3)[1];
                        greentotal += (dataPtr + 3)[1];
                        green = (byte)(greentotal / 9);
                        redtotal = 2 * (dataPtr)[2];
                        redtotal += 2 * (dataPtr - m.WidthStep)[2];
                        redtotal += 2 * (dataPtr + m.WidthStep)[2];
                        redtotal += (dataPtr + m.WidthStep + 3)[2];
                        redtotal += (dataPtr - m.WidthStep + 3)[2];
                        redtotal += (dataPtr + 3)[2];
                        red = (byte)(redtotal / 9);
                        dataPtr1[0] = blue;
                        dataPtr1[1] = green;
                        dataPtr1[2] = red;
                        dataPtr -= padding;
                        dataPtr1 -= padding;
                    }

                    dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                    dataPtr1 = (byte*)m1.ImageData.ToPointer(); // Pointer to the image
                    dataPtr += m.WidthStep + nChan;
                    dataPtr1 += m.WidthStep + nChan;
                    //tudo do meio
                    for (y = 1; y < (height - 1); y++)
                    {
                        for (x = 1; x < (width - 1); x++)
                        {
                            //retrieve 3 colour components
                            bluetotal = (dataPtr - m.WidthStep - 3)[0];
                            bluetotal += (dataPtr - m.WidthStep)[0];
                            bluetotal += (dataPtr - m.WidthStep + 3)[0];
                            bluetotal += (dataPtr - 3)[0];
                            bluetotal += (dataPtr + 3)[0];
                            bluetotal += (dataPtr)[0];
                            bluetotal += (dataPtr + m.WidthStep - 3)[0];
                            bluetotal += (dataPtr + m.WidthStep)[0];
                            bluetotal += (dataPtr + m.WidthStep + 3)[0];
                            bluetotal = bluetotal / 9;
                            blue = (byte)Math.Round(bluetotal);
                            greentotal = (dataPtr - m.WidthStep - 3)[1];
                            greentotal += (dataPtr - m.WidthStep)[1];
                            greentotal += (dataPtr - m.WidthStep + 3)[1];
                            greentotal += (dataPtr - 3)[1];
                            greentotal += (dataPtr + 3)[1];
                            greentotal += (dataPtr)[1];
                            greentotal += (dataPtr + m.WidthStep - 3)[1];
                            greentotal += (dataPtr + m.WidthStep)[1];
                            greentotal += (dataPtr + m.WidthStep + 3)[1];
                            greentotal = greentotal / 9;
                            green = (byte)Math.Round(greentotal);
                            redtotal = (dataPtr - m.WidthStep - 3)[2];
                            redtotal += (dataPtr - m.WidthStep)[2];
                            redtotal += (dataPtr - m.WidthStep + 3)[2];
                            redtotal += (dataPtr - 3)[2];
                            redtotal += (dataPtr + 3)[2];
                            redtotal += (dataPtr)[2];
                            redtotal += (dataPtr + m.WidthStep - 3)[2];
                            redtotal += (dataPtr + m.WidthStep)[2];
                            redtotal += (dataPtr + m.WidthStep + 3)[2];
                            redtotal = redtotal / 9;
                            red = (byte)Math.Round(redtotal);

                            // store in the image
                            dataPtr1[0] = (byte)blue;
                            dataPtr1[1] = (byte)green;
                            dataPtr1[2] = (byte)red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                            dataPtr1 += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding + 2 * nChan;
                        dataPtr1 += padding + 2 * nChan;
                    }
                }
            }
        }
        public static void NonUniform(Image<Bgr, byte> imgDest, Image<Bgr, byte> imgOrig, float[,] matrix, float matrixWeight, float offset)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = imgOrig.MIplImage;
                MIplImage m1 = imgDest.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                byte* dataPtr1 = (byte*)m1.ImageData.ToPointer(); // Pointer to the image
                float bluetotal, greentotal, redtotal;
                byte blue, green, red;

                int width = imgDest.Width;
                int height = imgDest.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    //primeiro pixel de cima
                    bluetotal = (dataPtr)[0] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]);
                    bluetotal = (dataPtr + m.WidthStep)[0] * (matrix[0, 2] + matrix[1, 2]);
                    bluetotal = (dataPtr + 3)[0] * (matrix[0, 2] + matrix[1, 2]);
                    bluetotal = (dataPtr + 3 + m.WidthStep)[0] * matrix[2, 2];
                    blue = (byte)((bluetotal / matrixWeight) + offset);
                    greentotal = (dataPtr)[1] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]);
                    greentotal = (dataPtr + m.WidthStep)[1] * (matrix[0, 2] + matrix[1, 2]);
                    greentotal = (dataPtr + 3)[1] * (matrix[0, 2] + matrix[1, 2]);
                    greentotal = (dataPtr + 3 + m.WidthStep)[1] * matrix[2, 2];
                    green = (byte)((greentotal / matrixWeight) + offset);
                    redtotal = (dataPtr)[2] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]);
                    redtotal = (dataPtr + m.WidthStep)[2] * (matrix[0, 2] + matrix[1, 2]);
                    redtotal = (dataPtr + 3)[2] * (matrix[0, 2] + matrix[1, 2]);
                    redtotal = (dataPtr + 3 + m.WidthStep)[2] * matrix[2, 2];
                    red = (byte)((redtotal / matrixWeight) + offset);
                    dataPtr1[0] = blue;
                    dataPtr1[1] = green;
                    dataPtr1[2] = red;
                    dataPtr += nChan;
                    dataPtr1 += nChan;

                    //linha de cima

                    float aux1 = matrix[0, 0] + matrix[0, 1];//auxiliares de calculo
                    float aux2 = matrix[1, 0] + matrix[1, 1];
                    float aux3 = matrix[2, 0] + matrix[2, 1];

                    for (x = 1; x < width - 1; x++)
                    {
                        bluetotal = (dataPtr)[0] * aux2;
                        bluetotal += (dataPtr - 3)[0] * aux1;
                        bluetotal += (dataPtr + 3)[0] * aux3;
                        bluetotal += (dataPtr + m.WidthStep + 3)[0] * matrix[0, 2];
                        bluetotal += (dataPtr + m.WidthStep)[0] * matrix[1, 2];
                        bluetotal += (dataPtr + m.WidthStep - 3)[0] * matrix[2, 2];
                        blue = (byte)((bluetotal / matrixWeight) + offset);
                        greentotal = (dataPtr)[1] * aux2;
                        greentotal += (dataPtr - 3)[1] * aux1;
                        greentotal += (dataPtr + 3)[1] * aux3;
                        greentotal += (dataPtr + m.WidthStep + 3)[1] * matrix[0, 2];
                        greentotal += (dataPtr + m.WidthStep)[1] * matrix[1, 2];
                        greentotal += (dataPtr + m.WidthStep - 3)[1] * matrix[2, 2];
                        green = (byte)((greentotal / matrixWeight) + offset);
                        redtotal = (dataPtr)[2] * aux2;
                        redtotal += (dataPtr - 3)[2] * aux1;
                        redtotal += (dataPtr + 3)[2] * aux3;
                        redtotal += (dataPtr + m.WidthStep + 3)[2] * matrix[0, 2];
                        redtotal += (dataPtr + m.WidthStep)[2] * matrix[1, 2];
                        redtotal += (dataPtr + m.WidthStep - 3)[2] * matrix[2, 2];
                        red = (byte)((redtotal / matrixWeight) + offset);
                        dataPtr1[0] = blue;
                        dataPtr1[1] = green;
                        dataPtr1[2] = red;
                        dataPtr += nChan;
                        dataPtr1 += nChan;
                    }

                    //ultimo pixel de cima

                    bluetotal = (dataPtr)[0] * (matrix[1, 0] + matrix[2, 0] + matrix[1, 1] + matrix[2, 1]);
                    bluetotal = (dataPtr + m.WidthStep)[0] * (matrix[1, 2] + matrix[2, 2]);
                    bluetotal = (dataPtr - 3)[0] * (matrix[0, 1] + matrix[0, 0]);
                    bluetotal = (dataPtr - 3 + m.WidthStep)[0] * matrix[0, 2];
                    blue = (byte)((bluetotal / matrixWeight) + offset);
                    greentotal = (dataPtr)[1] * (matrix[1, 0] + matrix[2, 0] + matrix[1, 1] + matrix[2, 1]);
                    greentotal = (dataPtr + m.WidthStep)[1] * (matrix[1, 2] + matrix[2, 2]);
                    greentotal = (dataPtr - 3)[1] * (matrix[0, 1] + matrix[0, 0]);
                    greentotal = (dataPtr - 3 + m.WidthStep)[1] * matrix[0, 2];
                    green = (byte)((greentotal / matrixWeight) + offset);
                    redtotal = (dataPtr)[2] * (matrix[1, 0] + matrix[2, 0] + matrix[1, 1] + matrix[2, 1]);
                    redtotal = (dataPtr + m.WidthStep)[2] * (matrix[1, 2] + matrix[2, 2]);
                    redtotal = (dataPtr - 3)[2] * (matrix[0, 1] + matrix[0, 0]);
                    redtotal = (dataPtr - 3 + m.WidthStep)[2] * matrix[0, 2];
                    red = (byte)((redtotal / matrixWeight) + offset);
                    dataPtr1[0] = blue;
                    dataPtr1[1] = green;
                    dataPtr1[2] = red;
                    dataPtr += padding;
                    dataPtr1 += padding;

                    // coluna da direita

                    aux1 = matrix[2, 0] + matrix[1, 0];//auxiliares de calculo
                    aux2 = matrix[1, 1] + matrix[2, 1];
                    aux3 = matrix[2, 2] + matrix[1, 2];

                    for (y = 1; y < height - 1; y++)
                    {
                        bluetotal = (dataPtr)[0] * aux2;
                        bluetotal += (dataPtr - m.WidthStep)[0] * aux1;
                        bluetotal += (dataPtr + m.WidthStep)[0] * aux3;
                        bluetotal += (dataPtr + m.WidthStep - 3)[0] * matrix[0, 2];
                        bluetotal += (dataPtr - m.WidthStep - 3)[0] * matrix[0, 0];
                        bluetotal += (dataPtr - 3)[0] * matrix[0, 1];
                        blue = (byte)((bluetotal / matrixWeight) + offset);
                        greentotal = (dataPtr)[1] * aux2;
                        greentotal += (dataPtr - m.WidthStep)[1] * aux1;
                        greentotal += (dataPtr + m.WidthStep)[1] * aux3;
                        greentotal += (dataPtr + m.WidthStep - 3)[1] * matrix[0, 2];
                        greentotal += (dataPtr - m.WidthStep - 3)[1] * matrix[0, 0];
                        greentotal += (dataPtr - 3)[1] * matrix[0, 1];
                        green = (byte)((greentotal / matrixWeight) + offset);
                        redtotal = (dataPtr)[2] * aux2;
                        redtotal += (dataPtr - m.WidthStep)[2] * aux1;
                        redtotal += (dataPtr + m.WidthStep)[2] * aux3;
                        redtotal += (dataPtr + m.WidthStep - 3)[2] * matrix[0, 2];
                        redtotal += (dataPtr - m.WidthStep - 3)[2] * matrix[0, 0];
                        redtotal += (dataPtr - 3)[2] * matrix[0, 1];
                        red = (byte)((redtotal / matrixWeight) + offset);
                        dataPtr1[0] = blue;
                        dataPtr1[1] = green;
                        dataPtr1[2] = red;
                        dataPtr += padding;
                        dataPtr1 += padding;
                    }
                    //ultimo pixel de baixo
                    bluetotal = (dataPtr)[0] * (matrix[1, 1] + matrix[2, 1] + matrix[1, 2] + matrix[2, 2]);
                    bluetotal += (dataPtr - 3)[0] * (matrix[0, 2] + matrix[0, 1]);
                    bluetotal += 2 * (dataPtr + m.WidthStep)[0] * (matrix[1, 0] + matrix[2, 0]);
                    bluetotal += (dataPtr - m.WidthStep - 3)[0] * matrix[0, 0];
                    blue = (byte)((bluetotal / matrixWeight) + offset);
                    greentotal = (dataPtr)[1] * (matrix[1, 1] + matrix[2, 1] + matrix[1, 2] + matrix[2, 2]);
                    greentotal += (dataPtr - 3)[1] * (matrix[0, 2] + matrix[0, 1]);
                    greentotal += 2 * (dataPtr + m.WidthStep)[1] * (matrix[1, 0] + matrix[2, 0]);
                    greentotal += (dataPtr - m.WidthStep - 3)[1] * matrix[0, 0];
                    green = (byte)((greentotal / matrixWeight) + offset);
                    redtotal = (dataPtr)[2] * (matrix[1, 1] + matrix[2, 1] + matrix[1, 2] + matrix[2, 2]);
                    redtotal += (dataPtr - 3)[2] * (matrix[0, 2] + matrix[0, 1]);
                    redtotal += 2 * (dataPtr + m.WidthStep)[2] * (matrix[1, 0] + matrix[2, 0]);
                    redtotal += (dataPtr - m.WidthStep - 3)[2] * matrix[0, 0];
                    red = (byte)((redtotal / matrixWeight) + offset);
                    dataPtr1[0] = blue;
                    dataPtr1[1] = green;
                    dataPtr1[2] = red;
                    dataPtr -= nChan;
                    dataPtr1 -= nChan;

                    //linha de baixo
                    aux1 = matrix[1, 1] + matrix[1, 2];//auxiliares de calculo
                    aux2 = matrix[0, 1] + matrix[0, 2];
                    aux3 = matrix[2, 2] + matrix[2, 1];

                    for (x = width - 1; x > 1; x--)
                    {
                        bluetotal = (dataPtr)[0] * aux1;
                        bluetotal += (dataPtr - 3)[0] * aux2;
                        bluetotal += (dataPtr + 3)[0] * aux3;
                        bluetotal += (dataPtr + m.WidthStep + 3)[0] * matrix[2, 0];
                        bluetotal += (dataPtr + m.WidthStep)[0] * matrix[1, 0];
                        bluetotal += (dataPtr + m.WidthStep - 3)[0] * matrix[0, 0];
                        blue = (byte)((bluetotal / matrixWeight) + offset);
                        greentotal = (dataPtr)[1] * aux1;
                        greentotal += (dataPtr - 3)[1] * aux2;
                        greentotal += (dataPtr + 3)[1] * aux3;
                        greentotal += (dataPtr + m.WidthStep + 3)[1] * matrix[2, 0];
                        greentotal += (dataPtr + m.WidthStep)[1] * matrix[1, 0];
                        greentotal += (dataPtr + m.WidthStep - 3)[1] * matrix[0, 0];
                        green = (byte)((greentotal / matrixWeight) + offset);

                        redtotal = (dataPtr)[2] * aux1;
                        redtotal += (dataPtr - 3)[2] * aux2;
                        redtotal += (dataPtr + 3)[2] * aux3;
                        redtotal += (dataPtr + m.WidthStep + 3)[2] * matrix[2, 0];
                        redtotal += (dataPtr + m.WidthStep)[2] * matrix[1, 0];
                        redtotal += (dataPtr + m.WidthStep - 3)[2] * matrix[0, 0];
                        red = (byte)((redtotal / matrixWeight) + offset);

                        dataPtr1[0] = blue;
                        dataPtr1[1] = green;
                        dataPtr1[2] = red;
                        dataPtr -= nChan;
                        dataPtr1 -= nChan;
                    }

                    //primeirao de baixo
                    bluetotal = (dataPtr)[0] * (matrix[0, 1] + matrix[1, 1] + matrix[0, 2] + matrix[1, 2]);
                    bluetotal += (dataPtr - m.WidthStep)[0] * (matrix[0, 0] + matrix[1, 0]);
                    bluetotal += (dataPtr + 3)[0] * (matrix[2, 1] + matrix[2, 2]);
                    bluetotal += (dataPtr - m.WidthStep + 3)[0] * matrix[2, 0];
                    blue = (byte)((bluetotal / matrixWeight) + offset);
                    greentotal = (dataPtr)[1] * (matrix[0, 1] + matrix[1, 1] + matrix[0, 2] + matrix[1, 2]);
                    greentotal += (dataPtr - m.WidthStep)[1] * (matrix[0, 0] + matrix[1, 0]);
                    greentotal += (dataPtr + 3)[1] * (matrix[2, 1] + matrix[2, 2]);
                    greentotal += (dataPtr - m.WidthStep + 3)[1] * matrix[2, 0];
                    green = (byte)((greentotal / matrixWeight) + offset);
                    redtotal = (dataPtr)[2] * (matrix[0, 1] + matrix[1, 1] + matrix[0, 2] + matrix[1, 2]);
                    redtotal += (dataPtr - m.WidthStep)[2] * (matrix[0, 0] + matrix[1, 0]);
                    redtotal += (dataPtr + 3)[2] * (matrix[2, 1] + matrix[2, 2]);
                    redtotal += (dataPtr - m.WidthStep + 3)[2] * matrix[2, 0];
                    red = (byte)((redtotal / matrixWeight) + offset);
                    dataPtr1[0] = blue;
                    dataPtr1[1] = green;
                    dataPtr1[2] = red;
                    dataPtr += padding;
                    dataPtr1 += padding;

                    // coluna da esquerda
                    aux1 = matrix[1, 0] + matrix[0, 0];//auxiliares de calculo
                    aux2 = matrix[1, 1] + matrix[0, 1];
                    aux3 = matrix[1, 2] + matrix[0, 2];

                    for (y = height - 1; y > 1; y--)
                    {
                        bluetotal = (dataPtr)[0] * aux2;
                        bluetotal += (dataPtr - m.WidthStep)[0] * aux1;
                        bluetotal += (dataPtr + m.WidthStep)[0] * aux2;
                        bluetotal += (dataPtr + m.WidthStep + 3)[0] * matrix[2, 2];
                        bluetotal += (dataPtr - m.WidthStep + 3)[0] * matrix[2, 0];
                        bluetotal += (dataPtr + 3)[0] * matrix[2, 1];
                        blue = (byte)((bluetotal / matrixWeight) + offset);

                        greentotal = (dataPtr)[1] * aux2;
                        greentotal += (dataPtr - m.WidthStep)[1] * aux1;
                        greentotal += (dataPtr + m.WidthStep)[1] * aux2;
                        greentotal += (dataPtr + m.WidthStep + 3)[1] * matrix[2, 2];
                        greentotal += (dataPtr - m.WidthStep + 3)[1] * matrix[2, 0];
                        greentotal += (dataPtr + 3)[1] * matrix[2, 1];
                        green = (byte)((greentotal / matrixWeight) + offset);

                        redtotal = (dataPtr)[2] * aux2;
                        redtotal += (dataPtr - m.WidthStep)[2] * aux1;
                        redtotal += (dataPtr + m.WidthStep)[2] * aux2;
                        redtotal += (dataPtr + m.WidthStep + 3)[2] * matrix[2, 2];
                        redtotal += (dataPtr - m.WidthStep + 3)[2] * matrix[2, 0];
                        redtotal += (dataPtr + 3)[2] * matrix[2, 1];
                        red = (byte)((redtotal / matrixWeight) + offset);

                        dataPtr1[0] = blue;
                        dataPtr1[1] = green;
                        dataPtr1[2] = red;
                        dataPtr -= padding;
                        dataPtr1 -= padding;
                    }
                    dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                    dataPtr1 = (byte*)m1.ImageData.ToPointer(); // Pointer to the image
                    dataPtr += m.WidthStep + nChan;
                    dataPtr1 += m.WidthStep + nChan;
                    //tudo do meio
                    for (y = 1; y < (height - 1); y++)
                    {
                        for (x = 1; x < (width - 1); x++)
                        {
                            //retrieve 3 colour components
                            bluetotal = (dataPtr - m.WidthStep - 3)[0] * matrix[0, 0];
                            bluetotal += (dataPtr - m.WidthStep)[0] * matrix[1, 0];
                            bluetotal += (dataPtr - m.WidthStep + 3)[0] * matrix[2, 0];
                            bluetotal += (dataPtr - 3)[0] * matrix[0, 1];
                            bluetotal += (dataPtr + 3)[0] * matrix[2, 1];
                            bluetotal += (dataPtr)[0] * matrix[1, 1];
                            bluetotal += (dataPtr + m.WidthStep - 3)[0] * matrix[0, 2];
                            bluetotal += (dataPtr + m.WidthStep)[0] * matrix[1, 2];
                            bluetotal += (dataPtr + m.WidthStep + 3)[0] * matrix[2, 2];
                            blue = (byte)((bluetotal / matrixWeight) + offset);
                            greentotal = (dataPtr - m.WidthStep - 3)[1] * matrix[0, 0];
                            greentotal += (dataPtr - m.WidthStep)[1] * matrix[1, 0];
                            greentotal += (dataPtr - m.WidthStep + 3)[1] * matrix[2, 0];
                            greentotal += (dataPtr - 3)[1] * matrix[0, 1];
                            greentotal += (dataPtr + 3)[1] * matrix[2, 1];
                            greentotal += (dataPtr)[1] * matrix[1, 1];
                            greentotal += (dataPtr + m.WidthStep - 3)[1] * matrix[0, 2];
                            greentotal += (dataPtr + m.WidthStep)[1] * matrix[1, 2];
                            greentotal += (dataPtr + m.WidthStep + 3)[1] * matrix[2, 2];
                            green = (byte)((greentotal / matrixWeight) + offset);
                            redtotal = (dataPtr - m.WidthStep - 3)[2] * matrix[0, 0];
                            redtotal += (dataPtr - m.WidthStep)[2] * matrix[1, 0];
                            redtotal += (dataPtr - m.WidthStep + 3)[2] * matrix[2, 0];
                            redtotal += (dataPtr - 3)[2] * matrix[0, 1];
                            redtotal += (dataPtr + 3)[2] * matrix[2, 1];
                            redtotal += (dataPtr)[2] * matrix[1, 1];
                            redtotal += (dataPtr + m.WidthStep - 3)[2] * matrix[0, 2];
                            redtotal += (dataPtr + m.WidthStep)[2] * matrix[1, 2];
                            redtotal += (dataPtr + m.WidthStep + 3)[2] * matrix[2, 2];
                            red = (byte)((redtotal / matrixWeight) + offset);

                            // store in the image
                            dataPtr1[0] = (byte)blue;
                            dataPtr1[1] = (byte)green;
                            dataPtr1[2] = (byte)red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                            dataPtr1 += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding + 2 * nChan;
                        dataPtr1 += padding + 2 * nChan;
                    }
                }
            }

        }
        public static void Sobel(Image<Bgr, byte> imgDestino, Image<Bgr, byte> imgOrigem)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = imgOrigem.MIplImage;
                MIplImage n = imgDestino.MIplImage;
                byte* dataPtrOrig = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                byte* dataPtrDest = (byte*)n.ImageData.ToPointer(); // Pointer to the image
                byte red, blue, green;
                int redx, bluex, greenx, redy, bluey, greeny, aux1, aux2, aux3;

                int width = imgOrigem.Width;
                int height = imgOrigem.Height;
                int nChan = m.NChannels; // number of channels - 3
                int widthstep = m.WidthStep;
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    //Tratamento da parte de dentro da imagem
                    dataPtrDest += widthstep + nChan;
                    dataPtrOrig += widthstep + nChan;

                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            bluex = (int)((dataPtrOrig - widthstep - nChan)[0] - (dataPtrOrig - widthstep + nChan)[0] + 2 * (dataPtrOrig - nChan)[0] - 2 * (dataPtrOrig + nChan)[0] + (dataPtrOrig + widthstep - nChan)[0] - (dataPtrOrig + widthstep + nChan)[0]);
                            greenx = (int)((dataPtrOrig - widthstep - nChan)[1] - (dataPtrOrig - widthstep + nChan)[1] + 2 * (dataPtrOrig - nChan)[1] - 2 * (dataPtrOrig + nChan)[1] + (dataPtrOrig + widthstep - nChan)[1] - (dataPtrOrig + widthstep + nChan)[1]);
                            redx = (int)((dataPtrOrig - widthstep - nChan)[2] - (dataPtrOrig - widthstep + nChan)[2] + 2 * (dataPtrOrig - nChan)[2] - 2 * (dataPtrOrig + nChan)[2] + (dataPtrOrig + widthstep - nChan)[2] - (dataPtrOrig + widthstep + nChan)[2]);

                            bluey = (int)(-(dataPtrOrig - widthstep - nChan)[0] - 2 * (dataPtrOrig - widthstep)[0] - (dataPtrOrig - widthstep + nChan)[0] + (dataPtrOrig + widthstep - nChan)[0] + 2 * (dataPtrOrig + widthstep)[0] + (dataPtrOrig + widthstep + nChan)[0]);
                            greeny = (int)(-(dataPtrOrig - widthstep - nChan)[1] - 2 * (dataPtrOrig - widthstep)[1] - (dataPtrOrig - widthstep + nChan)[1] + (dataPtrOrig + widthstep - nChan)[1] + 2 * (dataPtrOrig + widthstep)[1] + (dataPtrOrig + widthstep + nChan)[1]);
                            redy = (int)(-(dataPtrOrig - widthstep - nChan)[2] - 2 * (dataPtrOrig - widthstep)[2] - (dataPtrOrig - widthstep + nChan)[2] + (dataPtrOrig + widthstep - nChan)[2] + 2 * (dataPtrOrig + widthstep)[2] + (dataPtrOrig + widthstep + nChan)[2]);

                            aux1 = (Math.Abs(bluex) + Math.Abs(bluey));
                            aux2 = (Math.Abs(greenx) + Math.Abs(greeny));
                            aux3 = (Math.Abs(redx) + Math.Abs(redy));

                            if (aux1 > 255) aux1 = 255;
                            if (aux2 > 255) aux2 = 255;
                            if (aux3 > 255) aux3 = 255;

                            blue = (byte)aux1;
                            green = (byte)aux2;
                            red = (byte)aux3;

                            // store in the image
                            dataPtrDest[0] = blue;
                            dataPtrDest[1] = green;
                            dataPtrDest[2] = red;

                            // advance the pointer to the next pixel
                            dataPtrDest += nChan;
                            dataPtrOrig += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtrDest += nChan;
                        dataPtrOrig += nChan;
                        dataPtrDest += padding;
                        dataPtrOrig += padding;
                        dataPtrDest += nChan;
                        dataPtrOrig += nChan;
                    }

                    //Tratamento da ponta superior esquerda
                    dataPtrOrig = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                    dataPtrDest = (byte*)n.ImageData.ToPointer(); // Pointer to the image

                    bluex = (int)(3 * (dataPtrOrig)[0] + (-3) * (dataPtrOrig + nChan)[0] + (dataPtrOrig + widthstep)[0] + (-1) * (dataPtrOrig + widthstep + nChan)[0]);
                    greenx = (int)(3 * (dataPtrOrig)[0] + (-3) * (dataPtrOrig + nChan)[0] + (dataPtrOrig + widthstep)[0] + (-1) * (dataPtrOrig + widthstep + nChan)[0]);
                    redx = (int)(3 * (dataPtrOrig)[0] + (-3) * (dataPtrOrig + nChan)[0] + (dataPtrOrig + widthstep)[0] + (-1) * (dataPtrOrig + widthstep + nChan)[0]);

                    bluey = (int)((-3) * (dataPtrOrig)[0] + (-1) * (dataPtrOrig + nChan)[0] + 3 * (dataPtrOrig + widthstep)[0] + (dataPtrOrig + widthstep + nChan)[0]);
                    greeny = (int)((-3) * (dataPtrOrig)[0] + (-1) * (dataPtrOrig + nChan)[0] + 3 * (dataPtrOrig + widthstep)[0] + (dataPtrOrig + widthstep + nChan)[0]);
                    redy = (int)((-3) * (dataPtrOrig)[0] + (-1) * (dataPtrOrig + nChan)[0] + 3 * (dataPtrOrig + widthstep)[0] + (dataPtrOrig + widthstep + nChan)[0]);

                    aux1 = (Math.Abs(bluex) + Math.Abs(bluey));
                    aux2 = (Math.Abs(greenx) + Math.Abs(greeny));
                    aux3 = (Math.Abs(redx) + Math.Abs(redy));

                    if (aux1 > 255) aux1 = 255;
                    if (aux2 > 255) aux2 = 255;
                    if (aux3 > 255) aux3 = 255;

                    blue = (byte)aux1;
                    green = (byte)aux2;
                    red = (byte)aux3;

                    // store in the image
                    dataPtrDest[0] = blue;
                    dataPtrDest[1] = green;
                    dataPtrDest[2] = red;

                    //Tratamento da linha superior
                    dataPtrDest += nChan;
                    dataPtrOrig += nChan;

                    for (x = 1; x < width - 1; x++)
                    {
                        bluex = (int)((dataPtrOrig + nChan)[0] * (-3) + (dataPtrOrig - nChan)[0] * 3 + (-1) * (dataPtrOrig + widthstep + nChan)[0] + (dataPtrOrig + widthstep - nChan)[0]);
                        greenx = (int)((dataPtrOrig + nChan)[0] * (-3) + (dataPtrOrig - nChan)[0] * 3 + (-1) * (dataPtrOrig + widthstep + nChan)[0] + (dataPtrOrig + widthstep - nChan)[0]);
                        redx = (int)((dataPtrOrig + nChan)[0] * (-3) + (dataPtrOrig - nChan)[0] * 3 + (-1) * (dataPtrOrig + widthstep + nChan)[0] + (dataPtrOrig + widthstep - nChan)[0]);

                        bluey = (int)(dataPtrOrig[0] * (-2) + (dataPtrOrig + nChan)[0] * (-1) + (dataPtrOrig - nChan)[0] * (-1) + (dataPtrOrig + widthstep)[0] * 2 + (dataPtrOrig + widthstep + nChan)[0] + (dataPtrOrig + widthstep - nChan)[0]);
                        greeny = (int)(dataPtrOrig[0] * (-2) + (dataPtrOrig + nChan)[0] * (-1) + (dataPtrOrig - nChan)[0] * (-1) + (dataPtrOrig + widthstep)[0] * 2 + (dataPtrOrig + widthstep + nChan)[0] + (dataPtrOrig + widthstep - nChan)[0]);
                        redy = (int)(dataPtrOrig[0] * (-2) + (dataPtrOrig + nChan)[0] * (-1) + (dataPtrOrig - nChan)[0] * (-1) + (dataPtrOrig + widthstep)[0] * 2 + (dataPtrOrig + widthstep + nChan)[0] + (dataPtrOrig + widthstep - nChan)[0]);

                        aux1 = (Math.Abs(bluex) + Math.Abs(bluey));
                        aux2 = (Math.Abs(greenx) + Math.Abs(greeny));
                        aux3 = (Math.Abs(redx) + Math.Abs(redy));

                        if (aux1 > 255) aux1 = 255;
                        if (aux2 > 255) aux2 = 255;
                        if (aux3 > 255) aux3 = 255;

                        blue = (byte)aux1;
                        green = (byte)aux2;
                        red = (byte)aux3;

                        // store in the image
                        dataPtrDest[0] = blue;
                        dataPtrDest[1] = green;
                        dataPtrDest[2] = red;

                        // advance the pointer to the next pixel
                        dataPtrDest += nChan;
                        dataPtrOrig += nChan;
                    }

                    //Tratamento da ponta superior direita

                    bluex = (int)(-3 * (dataPtrOrig)[0] + 3 * (dataPtrOrig - nChan)[0] - (dataPtrOrig + widthstep)[0] + (dataPtrOrig + widthstep - nChan)[0]);
                    greenx = (int)(-3 * (dataPtrOrig)[1] + 3 * (dataPtrOrig - nChan)[1] - (dataPtrOrig + widthstep)[1] + (dataPtrOrig + widthstep - nChan)[1]);
                    redx = (int)(-3 * (dataPtrOrig)[2] + 3 * (dataPtrOrig - nChan)[2] - (dataPtrOrig + widthstep)[2] + (dataPtrOrig + widthstep - nChan)[2]);

                    bluey = (int)(-3 * (dataPtrOrig)[0] - (dataPtrOrig - nChan)[0] + 3 * (dataPtrOrig + widthstep)[0] + (dataPtrOrig + widthstep - nChan)[0]);
                    greeny = (int)(-3 * (dataPtrOrig)[1] - (dataPtrOrig - nChan)[1] + 3 * (dataPtrOrig + widthstep)[1] + (dataPtrOrig + widthstep - nChan)[1]);
                    redy = (int)(-3 * (dataPtrOrig)[2] - (dataPtrOrig - nChan)[2] + 3 * (dataPtrOrig + widthstep)[2] + (dataPtrOrig + widthstep - nChan)[2]);

                    aux1 = (Math.Abs(bluex) + Math.Abs(bluey));
                    aux2 = (Math.Abs(greenx) + Math.Abs(greeny));
                    aux3 = (Math.Abs(redx) + Math.Abs(redy));

                    if (aux1 > 255) aux1 = 255;
                    if (aux2 > 255) aux2 = 255;
                    if (aux3 > 255) aux3 = 255;

                    blue = (byte)aux1;
                    green = (byte)aux2;
                    red = (byte)aux3;

                    // store in the image
                    dataPtrDest[0] = blue;
                    dataPtrDest[1] = green;
                    dataPtrDest[2] = red;

                    dataPtrDest += nChan;
                    dataPtrOrig += nChan;

                    dataPtrDest += padding;
                    dataPtrOrig += padding;

                    //Tratamento da coluna da esquerda
                    for (x = 1; x < height - 1; x++)
                    {
                        bluex = (int)(dataPtrOrig[0] * 2 + (dataPtrOrig + widthstep)[0] + (dataPtrOrig - widthstep)[0] + (-2) * (dataPtrOrig + nChan)[0] + (-1) * (dataPtrOrig + widthstep + nChan)[0] + (-1) * (dataPtrOrig - widthstep + nChan)[0]);
                        greenx = (int)(dataPtrOrig[0] * 2 + (dataPtrOrig + widthstep)[0] + (dataPtrOrig - widthstep)[0] + (-2) * (dataPtrOrig + nChan)[0] + (-1) * (dataPtrOrig + widthstep + nChan)[0] + (-1) * (dataPtrOrig - widthstep + nChan)[0]);
                        redx = (int)(dataPtrOrig[0] * 2 + (dataPtrOrig + widthstep)[0] + (dataPtrOrig - widthstep)[0] + (-2) * (dataPtrOrig + nChan)[0] + (-1) * (dataPtrOrig + widthstep + nChan)[0] + (-1) * (dataPtrOrig - widthstep + nChan)[0]);

                        bluey = (int)(3 * (dataPtrOrig + widthstep)[0] + (dataPtrOrig - widthstep)[0] * (-3) + (dataPtrOrig + widthstep + nChan)[0] + (-1) * (dataPtrOrig - widthstep + nChan)[0]);
                        greeny = (int)(3 * (dataPtrOrig + widthstep)[0] + (dataPtrOrig - widthstep)[0] * (-3) + (dataPtrOrig + widthstep + nChan)[0] + (-1) * (dataPtrOrig - widthstep + nChan)[0]);
                        redy = (int)(3 * (dataPtrOrig + widthstep)[0] + (dataPtrOrig - widthstep)[0] * (-3) + (dataPtrOrig + widthstep + nChan)[0] + (-1) * (dataPtrOrig - widthstep + nChan)[0]);

                        aux1 = (Math.Abs(bluex) + Math.Abs(bluey));
                        aux2 = (Math.Abs(greenx) + Math.Abs(greeny));
                        aux3 = (Math.Abs(redx) + Math.Abs(redy));

                        if (aux1 > 255) aux1 = 255;
                        if (aux2 > 255) aux2 = 255;
                        if (aux3 > 255) aux3 = 255;

                        blue = (byte)aux1;
                        green = (byte)aux2;
                        red = (byte)aux3;

                        // store in the image
                        dataPtrDest[0] = blue;
                        dataPtrDest[1] = green;
                        dataPtrDest[2] = red;

                        // advance the pointer to the next pixel
                        dataPtrDest += widthstep;
                        dataPtrOrig += widthstep;
                    }

                    //Tratamento da ponta inferior esquerda
                    bluex = (int)(3 * (dataPtrOrig)[0] - 3 * (dataPtrOrig + nChan)[0] + (dataPtrOrig - widthstep)[0] - (dataPtrOrig - widthstep + nChan)[0]);
                    greenx = (int)(3 * (dataPtrOrig)[1] - 3 * (dataPtrOrig + nChan)[1] + (dataPtrOrig - widthstep)[1] - (dataPtrOrig - widthstep + nChan)[1]);
                    redx = (int)(3 * (dataPtrOrig)[2] - 3 * (dataPtrOrig + nChan)[2] + (dataPtrOrig - widthstep)[2] - (dataPtrOrig - widthstep + nChan)[2]);

                    bluey = (int)(3 * (dataPtrOrig)[0] + (dataPtrOrig + nChan)[0] - 3 * (dataPtrOrig - widthstep)[0] - (dataPtrOrig - widthstep + nChan)[0]);
                    greeny = (int)(3 * (dataPtrOrig)[1] + (dataPtrOrig + nChan)[1] - 3 * (dataPtrOrig - widthstep)[1] - (dataPtrOrig - widthstep + nChan)[1]);
                    redy = (int)(3 * (dataPtrOrig)[2] + (dataPtrOrig + nChan)[2] - 3 * (dataPtrOrig - widthstep)[2] - (dataPtrOrig - widthstep + nChan)[2]);

                    aux1 = (Math.Abs(bluex) + Math.Abs(bluey));
                    aux2 = (Math.Abs(greenx) + Math.Abs(greeny));
                    aux3 = (Math.Abs(redx) + Math.Abs(redy));

                    if (aux1 > 255) aux1 = 255;
                    if (aux2 > 255) aux2 = 255;
                    if (aux3 > 255) aux3 = 255;

                    blue = (byte)aux1;
                    green = (byte)aux2;
                    red = (byte)aux3;

                    // store in the image
                    dataPtrDest[0] = blue;
                    dataPtrDest[1] = green;
                    dataPtrDest[2] = red;

                    //Tratamento da linha inferior
                    dataPtrDest += nChan;
                    dataPtrOrig += nChan;

                    for (x = 1; x < width - 1; x++)
                    {
                        bluex = (int)((dataPtrOrig + nChan)[0] * (-3) + (dataPtrOrig - nChan)[0] * 3 + (dataPtrOrig - widthstep + nChan)[0] * (-1) + (dataPtrOrig - widthstep - nChan)[0]);
                        greenx = (int)((dataPtrOrig + nChan)[1] * (-3) + (dataPtrOrig - nChan)[1] * 3 + (dataPtrOrig - widthstep + nChan)[1] * (-1) + (dataPtrOrig - widthstep - nChan)[1]);
                        redx = (int)((dataPtrOrig + nChan)[2] * (-3) + (dataPtrOrig - nChan)[2] * 3 + (dataPtrOrig - widthstep + nChan)[2] * (-1) + (dataPtrOrig - widthstep - nChan)[2]);

                        bluey = (int)(dataPtrOrig[0] * 2 + (dataPtrOrig + nChan)[0] + (dataPtrOrig - nChan)[0] + (dataPtrOrig - widthstep)[0] * (-2) - (dataPtrOrig - widthstep + nChan)[0] - (dataPtrOrig - widthstep - nChan)[0]);
                        greeny = (int)(dataPtrOrig[1] * 2 + (dataPtrOrig + nChan)[1] + (dataPtrOrig - nChan)[1] + (dataPtrOrig - widthstep)[1] * (-2) - (dataPtrOrig - widthstep + nChan)[1] - (dataPtrOrig - widthstep - nChan)[1]);
                        redy = (int)(dataPtrOrig[2] * 2 + (dataPtrOrig + nChan)[2] + (dataPtrOrig - nChan)[2] + (dataPtrOrig - widthstep)[2] * (-2) - (dataPtrOrig - widthstep + nChan)[2] - (dataPtrOrig - widthstep - nChan)[2]);

                        aux1 = (Math.Abs(bluex) + Math.Abs(bluey));
                        aux2 = (Math.Abs(greenx) + Math.Abs(greeny));
                        aux3 = (Math.Abs(redx) + Math.Abs(redy));

                        if (aux1 > 255) aux1 = 255;
                        if (aux2 > 255) aux2 = 255;
                        if (aux3 > 255) aux3 = 255;

                        blue = (byte)aux1;
                        green = (byte)aux2;
                        red = (byte)aux3;

                        // store in the image
                        dataPtrDest[0] = blue;
                        dataPtrDest[1] = green;
                        dataPtrDest[2] = red;

                        // advance the pointer to the next pixel
                        dataPtrDest += nChan;
                        dataPtrOrig += nChan;
                    }

                    //Tratamento da ponta inferior direita
                    bluex = (int)((-3) * (dataPtrOrig)[0] + 3 * (dataPtrOrig - nChan)[0] + (-1) * (dataPtrOrig - widthstep)[0] + (dataPtrOrig - widthstep - nChan)[0]);
                    greenx = (int)((-3) * (dataPtrOrig)[0] + 3 * (dataPtrOrig - nChan)[0] + (-1) * (dataPtrOrig - widthstep)[0] + (dataPtrOrig - widthstep - nChan)[0]);
                    redx = (int)((-3) * (dataPtrOrig)[0] + 3 * (dataPtrOrig - nChan)[0] + (-1) * (dataPtrOrig - widthstep)[0] + (dataPtrOrig - widthstep - nChan)[0]);


                    bluey = (int)(3 * (dataPtrOrig)[0] + (dataPtrOrig - nChan)[0] + (-3) * (dataPtrOrig - widthstep)[0] + (-1) * (dataPtrOrig - widthstep - nChan)[0]);
                    greeny = (int)(3 * (dataPtrOrig)[0] + (dataPtrOrig - nChan)[0] + (-3) * (dataPtrOrig - widthstep)[0] + (-1) * (dataPtrOrig - widthstep - nChan)[0]);
                    redy = (int)(3 * (dataPtrOrig)[0] + (dataPtrOrig - nChan)[0] + (-3) * (dataPtrOrig - widthstep)[0] + (-1) * (dataPtrOrig - widthstep - nChan)[0]);

                    aux1 = (Math.Abs(bluex) + Math.Abs(bluey));
                    aux2 = (Math.Abs(greenx) + Math.Abs(greeny));
                    aux3 = (Math.Abs(redx) + Math.Abs(redy));

                    if (aux1 > 255) aux1 = 255;
                    if (aux2 > 255) aux2 = 255;
                    if (aux3 > 255) aux3 = 255;

                    blue = (byte)aux1;
                    green = (byte)aux2;
                    red = (byte)aux3;

                    // store in the image
                    dataPtrDest[0] = blue;
                    dataPtrDest[1] = green;
                    dataPtrDest[2] = red;

                    dataPtrDest -= widthstep;
                    dataPtrOrig -= widthstep;

                    //Tratamento da coluna da direita
                    for (x = 1; x < height - 1; x++)
                    {
                        bluex = (int)(dataPtrOrig[0] * (-2) + (dataPtrOrig + widthstep)[0] * (-1) + (dataPtrOrig - widthstep)[0] * (-1) + (dataPtrOrig - nChan)[0] * 2 + (dataPtrOrig + widthstep - nChan)[0] + (dataPtrOrig - widthstep - nChan)[0]);
                        greenx = (int)(dataPtrOrig[1] * (-2) + (dataPtrOrig + widthstep)[1] * (-1) + (dataPtrOrig - widthstep)[1] * (-1) + (dataPtrOrig - nChan)[1] * 2 + (dataPtrOrig + widthstep - nChan)[1] + (dataPtrOrig - widthstep - nChan)[1]);
                        redx = (int)(dataPtrOrig[2] * (-2) + (dataPtrOrig + widthstep)[2] * (-1) + (dataPtrOrig - widthstep)[2] * (-1) + (dataPtrOrig - nChan)[2] * 2 + (dataPtrOrig + widthstep - nChan)[2] + (dataPtrOrig - widthstep - nChan)[2]);

                        bluey = (int)((dataPtrOrig + widthstep)[0] * 3 + (dataPtrOrig - widthstep)[0] * (-3) + (dataPtrOrig + widthstep - nChan)[0] - (dataPtrOrig - widthstep - nChan)[0]);
                        greeny = (int)((dataPtrOrig + widthstep)[1] * 3 + (dataPtrOrig - widthstep)[1] * (-3) + (dataPtrOrig + widthstep - nChan)[1] - (dataPtrOrig - widthstep - nChan)[1]);
                        redy = (int)((dataPtrOrig + widthstep)[2] * 3 + (dataPtrOrig - widthstep)[2] * (-3) + (dataPtrOrig + widthstep - nChan)[2] - (dataPtrOrig - widthstep - nChan)[2]);

                        aux1 = (Math.Abs(bluex) + Math.Abs(bluey));
                        aux2 = (Math.Abs(greenx) + Math.Abs(greeny));
                        aux3 = (Math.Abs(redx) + Math.Abs(redy));

                        if (aux1 > 255) aux1 = 255;
                        if (aux2 > 255) aux2 = 255;
                        if (aux3 > 255) aux3 = 255;

                        blue = (byte)aux1;
                        green = (byte)aux2;
                        red = (byte)aux3;

                        // store in the image
                        dataPtrDest[0] = blue;
                        dataPtrDest[1] = green;
                        dataPtrDest[2] = red;

                        // advance the pointer to the next pixel
                        dataPtrDest -= widthstep;
                        dataPtrOrig -= widthstep;
                    }
                }
            }
        }
        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                int blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y, grey;
                int[] grey_level = new int[256];


                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = (int)dataPtr[0];//tenho que por em inteiro contraste* e brilho+
                            green = (int)dataPtr[1];
                            red = (int)dataPtr[2];

                            grey = (int)Math.Round((blue + green + red) / 3.0);

                            grey_level[grey]++;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
                return grey_level;
            }
        }
        public static int[,] Histogram_RGB(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                int blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y, grey;
                int[,] rgb_level = new int[3, 256];


                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = (int)dataPtr[0];//tenho que por em inteiro contraste* e brilho+
                            green = (int)dataPtr[1];
                            red = (int)dataPtr[2];

                            rgb_level[0, blue]++;
                            rgb_level[1, green]++;
                            rgb_level[2, red]++;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
                return rgb_level;
            }
        }
        public static int[,] Histogram_All(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                int blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y, grey;
                int[,] all_level = new int[4, 256];


                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = (int)dataPtr[0];//tenho que por em inteiro contraste* e brilho+
                            green = (int)dataPtr[1];
                            red = (int)dataPtr[2];

                            grey = (int)Math.Round((blue + green + red) / 3.0);

                            all_level[0, blue]++;
                            all_level[1, green]++;
                            all_level[2, red]++;
                            all_level[4, grey]++;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
                return all_level;
            }
        }
        public static void ConvertToBW(Emgu.CV.Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                int blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y, grey;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = (int)dataPtr[0];//tenho que por em inteiro contraste* e brilho+
                            green = (int)dataPtr[1];
                            red = (int)dataPtr[2];

                            grey = (int)Math.Round((blue + green + red) / 3.0);

                            if (grey <= threshold)
                                grey = 0;
                            else
                                grey = 255;

                            // advance the pointer to the next pixel

                            dataPtr[0] = (byte)grey;
                            dataPtr[1] = (byte)grey;
                            dataPtr[2] = (byte)grey;

                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }
        public static void ConvertToBW_Otsu(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image


                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)

                int x, y, t, threshold = 0;

                int[] grey_level = new int[256];
                double q1, q2, u1, u2, sigma, aux_sigma = 0, q_esquerda, q_direita, u_direita, u_esquerda;
                double n_pixel = width * height;

                grey_level = Histogram_Gray(img);
                // pesar nessa merda pra ser mais rapida

                for (t = 0; t < 255; t++)
                {
                    q_esquerda = 0;
                    u_esquerda = 0;
                    q_direita = 0;
                    u_direita = 0;

                    for (x = 0; x <= t; x++)
                    {
                        q_esquerda += grey_level[x];
                        u_esquerda += x * grey_level[x];
                    }

                    for (x = t + 1; x < 256; x++)
                    {
                        q_direita += grey_level[x];
                        u_direita += x * grey_level[x];
                    }

                    q1 = q_esquerda / n_pixel;
                    q2 = q_direita / n_pixel;
                    u1 = u_direita / q_direita;
                    u2 = u_esquerda / q_esquerda;

                    sigma = (q1 * q2 * Math.Pow(u1 - u2, 2));

                    if (sigma > aux_sigma)
                    {
                        aux_sigma = sigma;
                        threshold = t;
                    }

                }

                ConvertToBW(img, threshold);
            }
        }
        public static void FruitReader(Emgu.CV.Image<Bgr, byte> imgO, Emgu.CV.Image<Bgr, byte> imgAux, Emgu.CV.Image<Bgr, byte> imgAux1)
        {

            unsafe
            {
                //int d_max, d_min, d_contido, perimetro, area;

                double Fator_forma, Circularidade, d_contido, RaioModificacao, Quociente_aspecto, Compactacao, Extensao, perimetro, retangulo_inclusao, d_max, d_min, Total=0, Pixel_intensity_Red, Pixel_intensity_Green;
                //double MinTotal = 99999;
                double MinTotal = 0;
                int i,j,j_aux, C_nivelar,F_nivelar, Q_nivelar, Cp_nivelar , E_nivelar, RM_nivelar, area, IG_nivelar, IV_nivelar;
                //int i, j, j_aux, C_nivelar = 1, F_nivelar = 1, Q_nivelar = 1, Cp_nivelar = 1, E_nivelar = 1;
                double largura_pixel_cm, pixel_area;


                (largura_pixel_cm, pixel_area) = Lozangulo(imgAux);
                Console.WriteLine("Largura do pixel em cm :   " + largura_pixel_cm+"Area do pixel em cm^2:  "+ pixel_area);

                BlueChannel(imgAux);
                ConvertToBW_Otsu(imgAux);
                //o Erode tá a considerar os pretos como o vaziu da imagem
                imgAux.Erode(5).CopyTo(imgAux);
                imgAux.Dilate(5).CopyTo(imgAux);// suaviza as tretas

                Point minDiamP1 = new Point();
                Point minDiamP2 = new Point();
                Point maxDiamP1 = new Point();
                Point maxDiamP2 = new Point();
                Point raioContidoP2 = new Point();
                Point Centro = new Point();

                int num_frutas=Componentes_ligados(imgAux);
                List<(int, int)> contorno;

                List<(string, double, double, double,double, double, double,double, double)> Fruit_table = new List<(string, double, double, double, double, double, double, double,double)>// nome toal_min total_max
                {
                    //("Limão",0,30),
                    ("Banana",0.3781,0.2349,4.18, 0.3155 , 0.6149, 0.0099,247.87,225.35),// nome;Circularidade ;Fator_de_forma ; Quociente_aspecto; Extensao; Compactação; RaioModificacao
                    ("Banana",0.3765,0.2488,3.8074, 0.3598 ,0.6136,0.04436,225.43,189.20),
                    ("Maça",0.6197,0.4471,1.3587, 0.6301, 0.7872,0.6779,205.84,183.58),
                    ("Maça",0.9515,0.6151,1.0507, 0.7850, 0.9754, 0.9293,223.62,207.83),
                    ("Maça",0.9658,0.6407,1.0725, 0.8190, 0.9827,0.8571,165.53,53.61),
                    ("Maça",0.7461,0.5312,1.2152, 0.7196,  0.8637,0.7970,224.62,132.60),
                    ("Pera",0.5414,0.4432,1.5,0.6160, 0.7358,0.5774,240.97,214.20),
                    ("Pera",0.6863,0.5216,1.3229, 0.6905, 0.8284,0.6509,213.73,197.1),
                    ("Figo",0.7125,0.5507,1.2747,0.7238,0.8441,0.7155,103.16,75.37),
                    ("Figo",0.7263,0.4829,1.2873,0.6553,0.8522,0.7321, 138.931,113.790),
                    //("Limão",0.2983,0.2942,1.5078,0.3997,0.5461,237.51,201.27),*
                    //("Limão",0.2983,0.2942,1.5078,0.3997,0.5461,253.66,198.94),*
                    //("Morango",0.2957,0.2130,1.8666,0.2995,0.5437,194.62,52.64),*
                    //("Morango",0.5817, 0.2936,1.6, 0.5379,0.7627, 193.83,66.34),*

                    //("Figo",0.4181,0.2853,1.3292,0.3750,0.6466,0.03179),
                    //("Figo",0.6327,0.4829,1.6438,0.6553,0.7954,0.5833),

                    //("Banana",0.2327,0.2485,20.6666, 0.3125 , 0.4985, 0.0483),// nome;Circularidade ;Fator_de_forma ; Quociente_aspecto; Extensao; Compactação; RaioModificacao
                    //("Banana",0.2496,0.2484,2.1490, 0.3591 ,0.4996,0.0317),
                    //("Maça",0.6107,0.9309,1.0793, 0.7793, 0.9648,0.9191),
                    //("Maça",0.5516,0.4453,1.9652, 0.6275, 0.7427, 0.5088),
                    //("Maça",0.9250,0.6317,1.1333, 0.8075, 0.9617,0.8676),
                    //("Maça",0.7197,0.5273,1.3663, 0.7141, 0.8484,0.7246),
                    //("Pera",0.440,0.4500,2.3222,0.6123, 0.9485,0.6708),
                    //("Pera",0.5187,0.5928,1.6269, 0.6866, 0.7699,0.5658),
                    //("Figo",0.4181,0.2853,1.3292,0.3750,0.6466,0.03179),
                    //("Figo",0.6327,0.4829,1.6438,0.6553,0.7954,0.5833),
                    
                   
                    //("Lozangulo",1.8656,0.1973,1.9473,0.2638*Cp_nivelar)

                };
                double T1 = 1, T2 = 1, T3 = 1, T4 = 1, T5 = 1, T6 = 1,T7=0.1, T8=0;
                Console.WriteLine();

                for (int g = 0; g < Fruit_table.Count(); g++)
                {
                    for (int o = 0; o < Fruit_table.Count(); o++)
                    {
                        T1 += Math.Abs((Fruit_table[g].Item2 - Fruit_table[0].Item2) / Math.Pow(Fruit_table.Count(), 2));
                        T2 += Math.Abs((Fruit_table[g].Item3 - Fruit_table[0].Item3) / Math.Pow(Fruit_table.Count(), 2));
                        T3 += Math.Abs((Fruit_table[g].Item4 - Fruit_table[0].Item4) / Math.Pow(Fruit_table.Count(), 2));
                        T4 += Math.Abs((Fruit_table[g].Item5 - Fruit_table[0].Item5) / Math.Pow(Fruit_table.Count(), 2));
                        T5 += Math.Abs((Fruit_table[g].Item6 - Fruit_table[0].Item6) / Math.Pow(Fruit_table.Count(), 2));
                        T6 += Math.Abs((Fruit_table[g].Item7 - Fruit_table[0].Item7) / Math.Pow(Fruit_table.Count(), 2));
                        //T7 += Math.Abs((Fruit_table[g].Item8 - Fruit_table[0].Item8) / (1000*Math.Pow(Fruit_table.Count(), 2)));
                        //T8 += Math.Abs((Fruit_table[g].Item9 - Fruit_table[0].Item9) / (1000*Math.Pow(Fruit_table.Count(), 2)));
                    }
                }
                Console.WriteLine("media das diferenças ~V " + T7);
                Console.WriteLine("media das diferenças ~G  " + T8);
                C_nivelar = (int)T1;
                F_nivelar = (int)T2;
                Q_nivelar = (int)T3;
                E_nivelar = (int)T4;
                Cp_nivelar = (int)T5;
                RM_nivelar = (int)T6;
                IV_nivelar = (int)T7;
                IG_nivelar = (int)T8;
                Console.WriteLine(); 


                List<(string, int,double, int, double, double, double)> Fruits = new List<(string, int,double, int,double, double, double)>();// fruta, perimetro , area

               

                for (i = 0; i < num_frutas; i++)
                {
                    contorno = Contorno(imgAux, i);
                    retangulo_inclusao = AreaInclusão(contorno);
                    Console.WriteLine("A fruta " + i + " tem retangulo_inclusao de: " + retangulo_inclusao);

                   
                    perimetro = contorno.Count();
                    area = AreaPreenchida(imgAux,i);
                    (Centro.X, Centro.Y) = Centroide(imgAux, i, area);
                    (d_max, d_min, minDiamP1, minDiamP2, maxDiamP1, maxDiamP2) = Max_min_diametro(imgAux, contorno, i, area);
                    (d_contido, raioContidoP2) =Diametro_contido(imgAux, contorno, i, area);

                    Console.WriteLine("A fruta " + i + " tem d_mx: " + d_max + " e tem d_min: " + d_min);

                    Fator_forma = (4 * Math.PI * area) / (perimetro * perimetro);
                    Console.WriteLine("A fruta " + i + " tem fator de forma de: " + Fator_forma);
                    
                    Circularidade = (4 * area) / (Math.PI * d_max * d_max);
                    Console.WriteLine("A fruta " + i + " tem Circularidade de: " + Circularidade);

                    Quociente_aspecto = d_max / d_min;
                    Console.WriteLine("A fruta " + i + " tem Quociente_aspecto de: " + Quociente_aspecto);

                    Extensao = area / retangulo_inclusao;
                    Console.WriteLine("A fruta " + i + " tem Extensao de: " + Extensao);

                    Compactacao = (Math.Sqrt( (4/ Math.PI) *area))/d_max;
                    Console.WriteLine("A fruta " + i + " tem Compactacao de: " + Compactacao);

                    RaioModificacao = d_contido / d_max;
                    Console.WriteLine("A fruta " + i + " tem RaioModificacao de: " + RaioModificacao);

                    Pixel_intensity_Red = Get_PixelRedColor_intensity(imgO, imgAux, area, i);
                    Console.WriteLine("A fruta " + i + " tem Pixel_intensity_Red de: " + Pixel_intensity_Red);

                    Pixel_intensity_Green = Get_PixelGreenColor_intensity(imgO, imgAux, area, i);
                    Console.WriteLine("A fruta " + i + " tem Pixel_intensity_Green de: " + Pixel_intensity_Green);

                    Console.WriteLine();
                    
                    MinTotal = Math.Abs(Pixel_intensity_Green - Fruit_table[0].Item9) * IG_nivelar + Math.Abs(Pixel_intensity_Red - Fruit_table[0].Item8) * IV_nivelar + Math.Abs(RaioModificacao - Fruit_table[0].Item7) * RM_nivelar + Math.Abs(Compactacao - Fruit_table[0].Item6) * Cp_nivelar + Math.Abs(Extensao - Fruit_table[0].Item5) * E_nivelar + Math.Abs(Fator_forma - Fruit_table[0].Item3) * F_nivelar + Math.Abs(Circularidade - Fruit_table[0].Item2) * C_nivelar + Math.Abs(Quociente_aspecto - Fruit_table[0].Item4) * Q_nivelar;
                    Console.WriteLine("Total da diferença " + MinTotal + " da fruta: " + Fruit_table[0].Item1);

                    //Console.WriteLine("Modulos das diferenças: 0");
                    //Console.WriteLine(" Fator_forma: " + Math.Abs(Fator_forma - Fruit_table[0].Item3) * F_nivelar);
                    //Console.WriteLine(" Circularidade: " + Math.Abs(Circularidade - Fruit_table[0].Item2) * C_nivelar);
                    //Console.WriteLine(" Quociente_aspecto: " + Math.Abs(Quociente_aspecto - Fruit_table[0].Item4) * Q_nivelar);
                    //Console.WriteLine(" Extensao: " + Math.Abs(Extensao - Fruit_table[0].Item5) * E_nivelar);
                    //Console.WriteLine(" Compactacao: " + Math.Abs(Compactacao - Fruit_table[0].Item6) * Cp_nivelar);
                    //Console.WriteLine();

                    for (j = 1, j_aux = 0; j < Fruit_table.Count(); j++)
                    {
                        //ld_Total = Total;
                        Total = Math.Abs(Pixel_intensity_Green - Fruit_table[j].Item9) * IG_nivelar + Math.Abs(Pixel_intensity_Red - Fruit_table[j].Item8) * IV_nivelar + Math.Abs(RaioModificacao - Fruit_table[j].Item7) * RM_nivelar + Math.Abs(Compactacao - Fruit_table[j].Item6) * Cp_nivelar + Math.Abs(Extensao - Fruit_table[j].Item5) * E_nivelar + Math.Abs(Fator_forma - Fruit_table[j].Item3) * F_nivelar + Math.Abs(Circularidade - Fruit_table[j].Item2) * C_nivelar + Math.Abs(Quociente_aspecto - Fruit_table[j].Item4) * Q_nivelar;
                        Console.WriteLine("Total da diferença " + Total + " da fruta: " + Fruit_table[j].Item1);

                        //Console.WriteLine("Modulos das diferenças: " + j);
                        //Console.WriteLine(" Fator_forma: " + Math.Abs(Fator_forma - Fruit_table[j].Item3) * F_nivelar);
                        //Console.WriteLine(" Circularidade: " + Math.Abs(Circularidade - Fruit_table[j].Item2) * C_nivelar);
                        //Console.WriteLine(" Quociente_aspecto: " + Math.Abs(Quociente_aspecto - Fruit_table[j].Item4) * Q_nivelar);
                        //Console.WriteLine(" Extensao: " + Math.Abs(Extensao - Fruit_table[j].Item5) * E_nivelar);
                        //Console.WriteLine(" Compactacao: " + Math.Abs(Compactacao - Fruit_table[j].Item6) * Cp_nivelar);
                        //Console.WriteLine();

                        if (Total < MinTotal)
                        {
                            // List<(string, int, int)> newFruit = new List<(string, int, int)> { (Fruit_table[j].Item1, (int)perimetro, (int)area) };
                            // Add(Fruit_table[j].Item1, (int)perimetro, (int)area);
                            j_aux = j;
                            MinTotal = Total;
                        }
                    }
                    Console.WriteLine();

                    Fruits.Add((Fruit_table[j_aux].Item1, (int)perimetro,perimetro* largura_pixel_cm, (int)area, area* pixel_area, d_max, d_max * largura_pixel_cm));
                    imgO.Draw(new LineSegment2D(minDiamP1, minDiamP2), new Bgr(255, 0, 255), 1);
                    imgO.Draw(new LineSegment2D(maxDiamP1, maxDiamP2), new Bgr(255, 255, 0), 1);
                    imgO.Draw(new LineSegment2D(raioContidoP2, Centro), new Bgr(0, 255, 255), 1);
                    imgO.Draw(new Cross2DF(Centro, 10, 10), new Bgr(0, 0, 255), 2);
                }
                for (i = 0; i < num_frutas; i++)
                {
                    Rectangulo(imgO, imgAux, i);
                }

                for (i = 0; i < Fruits.Count; i++)
                {
                    Console.WriteLine($"Fruit: {Fruits[i].Item1}, perimetro_pixel: {Fruits[i].Item2}, perimetro_cm: {Fruits[i].Item3}  , Area_pixel^2: {Fruits[i].Item4}  ,Area_cm^2: {Fruits[i].Item5}  ,Diametro_máx_pixel: {Fruits[i].Item6}  ,Diametro_max_cm: {Fruits[i].Item7}");
                }
                Console.WriteLine();
  
            }
        }
        public static (double,double) Lozangulo(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int widthStep = m.WidthStep;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                double largura_em_cm = 1 , pixel_area_cm=0,largura_pixel_cm, D,d;

                int minX= width, num_pixel, minX_y =0, minY_x = 0, minY = height;


                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (dataPtr[0] == 0 && dataPtr[1] == 0 && dataPtr[2] == 0)
                        {
                            if (x < minX)
                                minX = x; minX_y= y;
                            if (y < minY) 
                                minY = y; minY_x = x;
                        }
                        dataPtr += nChan;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
                //Console.WriteLine("minY" + minX + "minX_y :" + minX_y);
                d = 2 * Math.Abs(minY - minX_y);
                D = 2 * Math.Abs(minX - minY_x);
                //num_pixel = (int)((D*d) / 2);
                dataPtr = (byte*)m.ImageData.ToPointer();
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (dataPtr[0] == 0 && dataPtr[1] == 0 && dataPtr[2] == 0)
                        {
                            dataPtr[0] = 255;
                            dataPtr[1] = 255;
                            dataPtr[2] = 255;
                        }

                        dataPtr += nChan;
                    }
                    dataPtr += padding;
                }
                //Console.WriteLine("D :" + D + "d :" + d);
                largura_pixel_cm = largura_em_cm / D;
                pixel_area_cm = largura_pixel_cm * largura_pixel_cm;
                return (largura_pixel_cm, pixel_area_cm);

            }
        }
        public static double Get_PixelRedColor_intensity(Image<Bgr, byte> imgAux1, Image<Bgr, byte> imgAux2, int area,int indice)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = imgAux1.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image copia da original

                MIplImage m2 = imgAux2.MIplImage;
                byte* dataPtr2 = (byte*)m2.ImageData.ToPointer(); // Pointer to the image Osu


                int width = imgAux1.Width;
                int height = imgAux1.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;
                double intensidade=0;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {   

                        if (dataPtr2[0] == indice)
                        {
                            intensidade += dataPtr[2];
                        }
                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                        dataPtr2 += nChan;
                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                    dataPtr2 += padding;
                }
                return intensidade / (float)area;
            }
        }
        public static double Get_PixelGreenColor_intensity(Image<Bgr, byte> imgAux1, Image<Bgr, byte> imgAux2, int area, int indice)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = imgAux1.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image copia da original

                MIplImage m2 = imgAux2.MIplImage;
                byte* dataPtr2 = (byte*)m2.ImageData.ToPointer(); // Pointer to the image Osu


                int width = imgAux1.Width;
                int height = imgAux1.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;
                double intensidade = 0;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {

                        if (dataPtr2[0] == indice)
                        {
                            intensidade += dataPtr[1];
                        }
                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                        dataPtr2 += nChan;
                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                    dataPtr2 += padding;
                }
                return intensidade / (float)area;
            }
        }
        public static (int,Point) Diametro_contido(Image<Bgr, byte> img, List<(int, int)> contorno, int index,int area)
        {
            unsafe
            {
                int d, i, j, x, y, r_min;
                float X_center, Y_center;

                int list_tamanho = contorno.Count();
                (X_center, Y_center) = Centroide(img, index, area);

                Point raioContidoP2 = new Point();
                //Console.WriteLine("Xcentro: " + X_center + " Ycentro " + Y_center);
                x = contorno[0].Item1;
                y = contorno[0].Item2;

                r_min = (int)(Math.Sqrt(Math.Pow((x - X_center), 2) + Math.Pow((y - Y_center), 2)));

                for (i = 1; i < list_tamanho; i++)
                {
                    x = contorno[i].Item1;
                    y = contorno[i].Item2;

                    d = (int)(Math.Sqrt(Math.Pow((x - X_center), (2)) + Math.Pow((y - Y_center), 2)));

                    if (d < r_min)
                    {
                     r_min = d;
                        raioContidoP2.X = x;
                        raioContidoP2.Y= y;
                    }
                       

                }

                return (r_min*2, raioContidoP2);
            }
        }
        public static (int, int, Point, Point, Point, Point) Max_min_diametro(Image<Bgr, byte> img, List<(int, int)> contorno, int index, int area)
        {
            unsafe
            {
                int d=0,d1, d2, i, j, x1, y1, x2, y2;
                float X_center, Y_center, arctan1=0, arctan2=0/*, b*/, comparador, comparador_x, tolerancia=(float)0.01,d_aux, d_max =0, d_min=10000,d_ponto, d_ponto2;

                int list_tamanho = contorno.Count();
                (X_center, Y_center) = Centroide(img, index, area);

                Point minDiamP1 = new Point();
                Point minDiamP2 = new Point();
                Point maxDiamP1 = new Point();
                Point maxDiamP2 = new Point();

                //Console.WriteLine("Xcentro: " + X_center + " Ycentro " + Y_center);
                //x = contorno[0].Item1;
                //y = contorno[0].Item2;

                //d_max = (int)(Math.Sqrt(Math.Pow((x - X_center), 2) + Math.Pow((y - Y_center), 2)));
                //d_min = d_max;

                for ( i = 0; i < list_tamanho; i++)
                {
                    x1 = contorno[i].Item1;
                    y1 = contorno[i].Item2;

                    // get slope from centroid to perimeterIndex point

                    arctan1 = (float)Math.Atan2(Y_center - y1, X_center - x1);
                    d_ponto2 = (float)Math.Sqrt(Math.Pow((Y_center - y1), 2) + Math.Pow((X_center - x1), 2));

                    for ( j = 0; j < list_tamanho; j++) // check slope to every point in perimeterIndex
                    {
                        x2 = contorno[j].Item1;
                        y2 = contorno[j].Item2;


                        d_ponto = -1;
                        arctan2 = -10000;
                        if (x2 != x1 && y2 != y1)
                        {
                            d_ponto = (float)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
                            arctan2 = (float)Math.Atan2(y2 - y1, x2 - x1);
                        }

                        if (arctan2 <= arctan1 + tolerancia && arctan2 >= arctan1 - tolerancia && d_ponto > d_ponto2) // check if slope intercects centroid within tolerance (tol)
                        {
                            d_aux = (float)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

                            if (d_aux < d_min)
                            {
                                d_min = d_aux;
                                minDiamP1.X = x1;
                                minDiamP1.Y = y1;
                                minDiamP2.X = x2;
                                minDiamP2.Y = y2;

                            }
                            if (d_aux > d_max)
                            {
                                d_max = d_aux;
                                maxDiamP1.X = x1;
                                maxDiamP1.Y = y1;
                                maxDiamP2.X = x2;
                                maxDiamP2.Y = y2;
                            }
                        }
                    }
                }

                return ((int)d_max, (int)d_min, minDiamP1, minDiamP2, maxDiamP1, maxDiamP2);
            }
        }
        public static double AreaInclusão(List<(int, int)> contorno)
        {
            unsafe{

                int x, y;
                int totalPontos = contorno.Count;

                int minX= contorno[0].Item1, maxX = contorno[0].Item1, minY= contorno[0].Item2, maxY= contorno[0].Item2;

                for (int i = 1; i < totalPontos; i++)
                {
                    x = contorno[i].Item1;
                    y = contorno[i].Item2;

                    if (x < minX)
                        minX = x;
                    if (x > maxX)
                        maxX = x;
                    if (y < minY)
                        minY = y;
                    if (y > maxY)
                        maxY = y;
                }
                double largura = (double)(maxX - minX);
                double altura = (double)(maxY - minY);

                return largura * altura;

            }
        }
        public static int Perimetro4(Image<Bgr, byte> img)
        {
            unsafe
            {
                List<(int, int)> contorno = Contorno(img,0);
                return contorno.Count;
            }
        }
        // vandtagem de ter o centroide commo contorno é que mesmo que a binarização tenha deichado buracos isso n é relevante se n afetar o contorno
        public static (int, int) Centroide(Image<Bgr, byte> img, int index,int area)
        {
            unsafe
            {
                Point Center = new Point();

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer();

                List<(int, int)> contorno = Contorno(img, index);
                int totalPontos = contorno.Count;
               

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y, x_aux=0, y_aux=0;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            if (dataPtr[0]== index)
                            {
                                x_aux += x;
                                y_aux += y;
                            }
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
                int centroX = (int)(x_aux / area);
                int centroY = (int)(y_aux / area);
                Center.X = centroX;
                Center.Y = centroY;
                
                return (centroX, centroY);
            }
        }

        public static void Rectangulo(Image<Bgr, byte> img, Image<Bgr, byte> imgAux, int numFruta)
        {
            unsafe
            {
                
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                MIplImage m1 = imgAux.MIplImage;
                byte* dataPtr2 = (byte*)m1.ImageData.ToPointer(); // Pointer to the image

                int width = imgAux.Width;
                int height = imgAux.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y, i, u;
                int minX = width, maxX = 0, minY = height, maxY = 0;

                List<(int, int)> contorno = Contorno(imgAux, numFruta);
                //Console.WriteLine("a fruta: " +(numFruta+1)+" Tem de perimetro : " +contorno.Count);
                int totalPontos = contorno.Count;
                


                //encontrar valores mais altos e baixos de X e Y
                for (i = 0; i < totalPontos; i++)
                {
                    x = contorno[i].Item1;
                    y = contorno[i].Item2;

                    if (x < minX)
                        minX = x;
                    if (x > maxX)
                        maxX = x;
                    if (y < minY)
                        minY = y;
                    if (y > maxY)
                        maxY = y;
                }
                //desenhar linha de cima
                dataPtr = dataPtr + (m.WidthStep * (minY - 1)) + (nChan * (minX - 1));

                for (u = minX; u <= maxX; u++)
                {
                    dataPtr[0] = 50;
                    dataPtr[1] = 205;
                    dataPtr[2] = 50;
                    dataPtr += nChan;
                }
                //linha da direita
                for (u = minY; u <= maxY; u++)
                {
                    dataPtr[0] = 50;
                    dataPtr[1] = 205;
                    dataPtr[2] = 50;
                    dataPtr += m.WidthStep;
                }
                //linha de baixo
                for (u = maxX; u >= minX; u--)
                {
                    dataPtr[0] = 50;
                    dataPtr[1] = 205;
                    dataPtr[2] = 50;
                    dataPtr -= nChan;
                }
                //linha da esquerda
                for (u = maxY; u >= minY; u--)
                {
                    dataPtr[0] = 50;
                    dataPtr[1] = 205;
                    dataPtr[2] = 50;
                    dataPtr -= m.WidthStep;
                }

            }
        }

        // quero saber como criar uma imagem de um tamanho específico
        public static List<(int, int)> Contorno(Image<Bgr, byte> img, int index)
        {
            unsafe
            {

                // ConvertToBW_Otsu(img);

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int widthStep = m.WidthStep;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y, x_aux = 0, y_aux = 0, blue, aux_1 = 0;

                List<(int, int)> contorno = new List<(int, int)>();

                for (y_aux = 0; (y_aux < height && aux_1 == 0); y_aux++)
                {
                    for (x_aux = 0; (x_aux < width && aux_1 == 0); x_aux++)
                    {
                        blue = dataPtr[0];

                        if (blue == index)
                        {
                            aux_1 = 1;
                            dataPtr -= nChan;
                            dataPtr -= padding;
                        }


                        // advance the pointer to the next pixel
                        dataPtr += nChan;

                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;

                }
                contorno.Add((x_aux, y_aux));

                //Ta a vir
                // eqr => 0
                // baixp => 1
                // direita => 2
                // cima => 3
                int direcao = 0;
                x = x_aux;
                y = y_aux;


                do
                {
                    switch (direcao)
                    {
                        case 0:
                            {
                                if ((dataPtr - widthStep)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr -= widthStep;
                                    direcao = 1;
                                    y--;
                                    contorno.Add((x, y));
                                }
                                else if ((dataPtr + nChan)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr += nChan;
                                    x++;
                                    contorno.Add((x, y));

                                }
                                else if ((dataPtr + widthStep)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr += widthStep;
                                    direcao = 3;
                                    y++;
                                    contorno.Add((x, y));
                                }
                                else if ((dataPtr - nChan)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr -= nChan;
                                    direcao = 2;
                                    x--;
                                    contorno.Add((x, y));
                                }

                            }
                            break;

                        case (1):
                            {

                                if ((dataPtr - nChan)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr -= nChan;
                                    direcao = 2;
                                    x--;
                                    contorno.Add((x, y));
                                }
                                else if ((dataPtr - widthStep)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr -= widthStep;
                                    y--;
                                    contorno.Add((x, y));
                                }
                                else if ((dataPtr + nChan)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr += nChan;
                                    direcao = 0;
                                    x++;
                                    contorno.Add((x, y));
                                }

                                else if ((dataPtr + widthStep)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr += widthStep;
                                    direcao = 3;
                                    y++;
                                    contorno.Add((x, y));
                                }


                            }
                            break;

                        case (2):
                            {
                                if ((dataPtr + widthStep)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr += widthStep;
                                    direcao = 3;
                                    y++;
                                    contorno.Add((x, y));
                                }
                                else if ((dataPtr - nChan)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr -= nChan;
                                    x--;
                                    contorno.Add((x, y));
                                }
                                else if ((dataPtr - widthStep)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr -= widthStep;
                                    direcao = 1;
                                    y--;
                                    contorno.Add((x, y));
                                }
                                else if ((dataPtr + nChan)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr += nChan;
                                    direcao = 0;
                                    x++;
                                    contorno.Add((x, y));
                                }


                            }
                            break;

                        case (3):
                            {
                                if ((dataPtr + nChan)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr += nChan;
                                    direcao = 0;
                                    x++;
                                    contorno.Add((x, y));
                                }
                                else if ((dataPtr + widthStep)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr += widthStep;
                                    y++;
                                    contorno.Add((x, y));
                                }
                                else if ((dataPtr - nChan)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr -= nChan;
                                    direcao = 2;
                                    x--;
                                    contorno.Add((x, y));
                                }
                                else if ((dataPtr - widthStep)[0] == index)
                                {
                                    //perimetro++;
                                    dataPtr -= widthStep;
                                    direcao = 1;
                                    y--;
                                    contorno.Add((x, y));
                                }


                            }
                            break;

                    }
                    // para visualização
                    //dataPtr[0] = 0;
                    //dataPtr[1] = 0;
                    //dataPtr[2] = 255;


                } while ((x != x_aux || y != y_aux));

                return contorno;
            }
        }

        public static int Componentes_ligados(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                // byte* dataPtraux;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int widthsttep = m.WidthStep;
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y, pix_valeu, indice = 0, indice2, indice3;

                //int* vetor_vetor; 
                // tive que fazer uma matriz por conta do numero de possiveis indices que as ficura com mais de uma fruta exigem

                List<int[]> correspondencias = new List<int[]>();

                int[,] matriz_aux = new int[width, height];

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        pix_valeu = (int)dataPtr[0];
                        matriz_aux[x, y] = pix_valeu;

                        if (pix_valeu != 255)
                        {

                            indice2 = matriz_aux[x, y - 1];           //indice2 = (int)(dataPtr - widthsttep)[0];
                            indice3 = matriz_aux[x - 1, y];          //indice3 = (int)(dataPtr - nChan)[0];

                            if (indice2 > pix_valeu && indice2 != 255)
                            {
                                matriz_aux[x, y] = indice2;

                                if (indice2 != indice3 && indice3 != 255)
                                    AdicionarAoVetorDeVetores(correspondencias, indice2, indice3);

                            }
                            else if (indice3 > pix_valeu && indice3 != 255)
                            {
                                matriz_aux[x, y] = indice3;

                            }
                            else
                            {
                                matriz_aux[x, y] = indice;
                                if (indice == 254)
                                indice = 256;

                                else 
                                indice++;
                            }
                        }

                        dataPtr += nChan;
                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
                matriz_aux = correspondencias_matriz(correspondencias,matriz_aux);

                dataPtr = (byte*)m.ImageData.ToPointer();

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        dataPtr[0] = (byte)matriz_aux[x, y];
                        dataPtr[1] = (byte)matriz_aux[x, y];
                        dataPtr[2] = (byte)matriz_aux[x, y];

                        dataPtr += nChan;
                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }

                // visualizar a matriz_aux
                //for (int i = 0; i < height; i++)
                //{
                //    for (int j = 0; j < width; j++)
                //    {
                //        // Imprimir o valor atual seguido de um espaço
                //        Console.Write(matriz_aux[j, i] + " ");
                //    }

                //    // Adicionar uma quebra de linha após cada linha da matriz
                //    Console.WriteLine();
                //}


                // visualizar a lista de correspondencias
                //foreach (var vetor in correspondencias)
                //{
                //    // Imprimir os elementos do vetor
                //    foreach (var elemento in vetor)
                //    {
                //        Console.Write(elemento + " ");
                //    }

                //    // Adicionar uma quebra de linha após cada vetor
                //    Console.WriteLine();
                //}
                    
                return correspondencias.Count;

                
                
                

                // projeção horizontal e vertical para saber onde cortar a imagem
                // calcular o retangulo do objeto
            }
        }

        public static int [,] correspondencias_matriz(List<int[]> correspondencia,int[,] matriz_aux)
        {
            // saco todos os minimos
            // encontro a posição do pixel na correspondencia e sutituo na matriz o valor minimo
            // elimino todos os valores que pertenssem a um vetor com menos de X valores

            int width = matriz_aux.GetLength(0);
            int height = matriz_aux.GetLength(1);

            int posicao_correspondencia;
            int num_componets = correspondencia.Count();

            int[] minV = new int[num_componets];

            for (int aux = 0; aux < num_componets; aux++)
            {
                minV[aux] = correspondencia[aux].Min();
            }

            for (int j = 0; j < num_componets; j++)
            {
                if (correspondencia[j].Length < 10)
                {
                    correspondencia.RemoveAt(j);
                    num_componets--;
                    j--;
                }

            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // Imprimir o valor atual seguido de um espaço
                    //Console.Write(matriz_aux[j, i] + " ");
                    if (matriz_aux[j, i] != 255)
                    {
                        posicao_correspondencia = Valor2Existe(correspondencia, matriz_aux[j, i]);
                        if (posicao_correspondencia != -1)
                                matriz_aux[j, i] = posicao_correspondencia;// substituo o nível

                        else
                        {
                            matriz_aux[j, i] = 255;// tenho que eliminar as posiçoes da lista que n tem mais de 10 valores 
                        }
                    }
                }
                
                // Adicionar uma quebra de linha após cada linha da matriz
                //Console.WriteLine();
            }
            
            Console.Write("Numero de elementos da imagem "+ correspondencia.Count +"  ");
            Console.WriteLine();
            return matriz_aux;
            
        }

        static int Valor2Existe(List<int[]> vetorDeVetores, int valor2)
        {
            for (int i = 0; i < vetorDeVetores.Count; i++)
            {
                for (int j = 0; j < vetorDeVetores[i].Length; j++)
                {
                    if (vetorDeVetores[i][j] == valor2)
                    {
                        return i; // Retorna a posição onde valor2 foi encontrado
                    }
                }
            }
            return -1; // Retorna -1 se valor2 não foi encontrado
        }

        // Função para adicionar valor1 e valor2 ao vetor de vetores
        static void AdicionarAoVetorDeVetores(List<int[]> vetorDeVetores, int valor1, int valor2)
        {
            // Verifica se valor2 já existe em algum vetor 
            int posicao2 = Valor2Existe(vetorDeVetores, valor2);
            // Verifica se valor1 já existe em algum vetor 
            int posicao1 = Valor2Existe(vetorDeVetores, valor1);

            if (posicao2 != -1 && posicao1 == -1)
            {
                // Se valor2 já existe, concatena valor1 no vetor correspondente
                vetorDeVetores[posicao2] = vetorDeVetores[posicao2].Concat(new[] { valor1 }).ToArray();
            }
            else if (posicao1 != -1 && posicao2 == -1)
            {
                vetorDeVetores[posicao1] = vetorDeVetores[posicao1].Concat(new[] { valor2 }).ToArray();
            }
            else if (posicao2 == -1 && posicao1 == -1)
            {
                // Se valor2 não existe, cria um novo vetor com [valor1, valor2]
                vetorDeVetores.Add(new[] { valor1, valor2 });
            }
            else if (posicao1 != posicao2)
            {
                vetorDeVetores[posicao1] = vetorDeVetores[posicao2].Concat(vetorDeVetores[posicao1]).ToArray();

                vetorDeVetores.RemoveAt(posicao2);
            }
        }

        // Acho que isso ainda n esta atualizado
        public static int AreaPreenchida(Emgu.CV.Image<Bgr, byte> img, int index)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int AreaRectangulo = 0;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int widthstep = m.WidthStep;
                int padding = m.WidthStep - m.NChannels * m.Width; // alinhament bytes (padding)
                int x, y;
                // ConvertToBW_Otsu(img);
                //dataPtr += minX * nChan + minY * widthstep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if ((int)dataPtr[0] == index)
                                AreaRectangulo++;
                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }

                }
                return AreaRectangulo;
            }
        }

       
    }

    /// <summary>
    /// Fruit Reader
    /// </summary>
    /// <param name="imgDest">imagem de destino (cópia da original)</param>
    /// <param name="imgOrig">imagem original </param>
    /// <param name="level">nivel de dificuldade da imagem</param>
    /// <param name="fruitResult">Objecto resultado - lista de frutas e repectivas informações</param>
    //public static void FruitReader(Image<Bgr, byte> imgDest, Image<Bgr, byte> imgOrig,
    //    int level, out FruitResults fruitResult)
    //{

    //    fruitResult = new FruitResults();

    //    //fruit creation
    //    Fruit fruit = new Fruit();
    //    fruit.fruitEnum = FruitEnum.banana;
    //    fruit.fruitRect = new Rectangle(10, 10, 200, 200);
    //    fruit.features.Add(FeaturesEnum.Circularidade, 1);

    //    if (level >= 3)
    //        fruit.metricFeatures.Add(MetricFeaturesEnum.area, 110);

    //    imgDest.Draw(fruit.fruitRect, new Bgr(Color.Green));

    //    // add fruit to results
    //    fruitResult.results.Add(fruit);


    //}
}


