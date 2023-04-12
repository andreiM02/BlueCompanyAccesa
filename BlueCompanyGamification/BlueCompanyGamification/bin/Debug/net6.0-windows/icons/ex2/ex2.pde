PImage img;  // declare a PImage object
int screen_width = 800;
int screen_height = 600;
int[] histogram_r = new int[256];
int[] histogram_g = new int[256];
int[] histogram_b = new int[256];

void setup() {
  size(800, 800);
  img = loadImage("zambet.png");  // load the image from the "data" folder
  img.filter(GRAY);  // convert the image to grayscale
  calculateHistogram();
}

void draw() {
  background(255);  // clear the screen
  
  // draw the red histogram
  stroke(255, 0, 0);
  drawHistogram(histogram_r, 0, 100, screen_width, 200);
  
  // draw the green histogram
  stroke(0, 255, 0);
  drawHistogram(histogram_g, 0, 300, screen_width, 200);
  
  // draw the blue histogram
  stroke(0, 0, 255);
  drawHistogram(histogram_b, 0, 500, screen_width, 200);
}

void calculateHistogram() {
  // calculate the histogram for the red channel
  for (int y = 0; y < img.height; y++) {
    for (int x = 0; x < img.width; x++) {
      int red_value = (int) red(img.get(x, y));
      histogram_r[red_value]++;
    }
  }
  
  // calculate the histogram for the green channel
  for (int y = 0; y < img.height; y++) {
    for (int x = 0; x < img.width; x++) {
      int green_value = (int) green(img.get(x, y));
      histogram_g[green_value]++;
    }
  }
  
  // calculate the histogram for the blue channel
  for (int y = 0; y < img.height; y++) {
    for (int x = 0; x < img.width; x++) {
      int blue_value = (int) blue(img.get(x, y));
      histogram_b[blue_value]++;
    }
  }
}

void drawHistogram(int[] histogram, int x, int y, int w, int h) {
  float max_count = max(histogram);  // get the maximum count in the histogram
  for (int i = 0; i < histogram.length; i++) {
    float x1 = map(i, 0, histogram.length - 1, x, x + w); // map the x value to the screen width
float y1 = map(histogram[i], 0, max_count, y + h, y); // map the y value to the screen height
float x2 = x1;
float y2 = y + h;
line(x1, y1, x2, y2); // draw a line for the current value in the histogram
}
}
