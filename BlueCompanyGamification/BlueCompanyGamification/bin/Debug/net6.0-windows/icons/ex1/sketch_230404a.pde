PImage img;  // declare a PImage object
int screen_width = 800;
int screen_height = 600;

void setup() {
  size(800, 600);
  img = loadImage("ufo.png");  // load the image from the "data" folder
}

void draw() {
  background(255);  // clear the screen
  
  // check if the image has been loaded before trying to use it
  if (img != null) {
    // calculate the new dimensions of the image to fit the screen while preserving the aspect ratio
    float aspect_ratio = (float) img.width / img.height;
    float new_width = screen_width;
    float new_height = new_width / aspect_ratio;
    if (new_height > screen_height) {
      new_height = screen_height;
      new_width = new_height * aspect_ratio;
    }

    // calculate the x and y coordinates to center the image
    float x = (screen_width - new_width) / 2;
    float y = (screen_height - new_height) / 2;

    // draw the image
    image(img, x, y, new_width, new_height);
  }
}
