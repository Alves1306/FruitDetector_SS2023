# 🍎 FruitDetector_SS2023

Computer vision system developed in **C# with Emgu.CV** (OpenCV for .NET), as part of the **Sensory Systems** course in the 4th year of the Integrated Master's in Electrical and Computer Engineering.

The system automatically detects and identifies fruits in images by analyzing shape and color features.

## 🎯 Objective

Develop an application capable of:

- Processing images containing fruits  
- Detecting objects/fruits present  
- Calculating features such as area, perimeter, circularity, average color, etc.  
- Comparing these metrics with a reference database  
- Automatically identifying each fruit based on quantitative criteria  

## 🧠 Main Features

- Automatic binarization using **Otsu**
- Morphological filters (erosion, dilation)
- Detection of **connected components**
- Measurement of:
  - Real area (cm²)
  - Perimeter
  - Circularity
  - Centroid
  - Aspect ratio
  - Shape factor
  - Average color (red and green channels)
- Pixel-to-centimeter conversion using a calibration object (diamond marker)
- Fruit identification based on weighted comparison with reference values
- Drawing bounding boxes and rectangles over detected fruits

## 🖥️ Interface

The graphical interface was developed using **Windows Forms**, allowing:

- Image loading  
- Execution of filters and operations  
- Real-time display of measurement results  
- Interaction through dropdown menus  

## 📂 Project Structure

```
FruitDetector_SS2023/
├── SS_OpenCV_2023.sln       # Visual Studio Solution
├── SS_OpenCV/               # Main project
│   ├── MainForm.cs          # GUI and event handlers
│   ├── ImageClass.cs        # Image processing logic (FruitReader, metrics, etc.)
├── FruitDLL/                # Auxiliary library
├── pack_imagens/            # Test images
├── SS_operations.csv        # Data export
├── vcredist_x86_2012.exe    # Redistributable required for EmguCV
└── README.md                # This file
```

## 🚀 How to Run the Project

1. Clone this repository:
   ```bash
   git clone https://github.com/Alves1306/FruitDetector_SS2023.git
   ```

2. Open the project:
   - Launch `SS_OpenCV_2023.sln` using Visual Studio.

3. Check NuGet packages:
   - Ensure that `Emgu.CV` and its dependencies are installed.

4. Build the solution.

5. Run with `F5` and load an image containing fruits.

6. Use the **“Fruit Reader”** menu to start the detection and classification process.

## 📊 Sample Output

- Fruits outlined with rectangles  
- Fruit name and measurements displayed in the console  
- Metrics calculated in real-world units (cm)  
- Identification based on internal reference table (`Fruit_table`)  

## 👥 Authors

Project developed by:

- António Alves — nº 58339  
- Márcio Costa — nº 60446  

## 📚 License

This project was developed for academic purposes and is not publicly licensed.  
Restricted to educational use only.
