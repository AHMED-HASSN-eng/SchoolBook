// this section to choose stage
let highStage = document.querySelector(".high-stage");
let middleStage = document.querySelector(".middle-stage");
let primaryStage = document.querySelector(".primary-stage");
let department = document.getElementById("department");

highStage.addEventListener('click', function (){
  department.classList.remove('hidden');
})
primaryStage.addEventListener('click', function (){
  department.classList.add('hidden');
})
middleStage.addEventListener('click', function (){
  department.classList.add('hidden');
})

// this section to choose level
let highMidLevel = document.getElementById("mid-high-level");
let primaryLevel = document.getElementById("primary-level");

highStage.addEventListener('click', function (){
  highMidLevel.classList.remove("hidden");
  primaryLevel.classList.add("hidden");
})

primaryStage.addEventListener('click', function (){
  highMidLevel.classList.add("hidden");
  primaryLevel.classList.remove("hidden");
})

middleStage.addEventListener('click', function (){
  highMidLevel.classList.remove("hidden");
  primaryLevel.classList.add("hidden");
})

// this section to choose subject for sutable stage
let primaryMiddleSubjects = document.getElementById("primary-middle-subject");
// this variables store labels or buttons
let firstHighStage = document.getElementById("first-high-stage")
let scientificDepartment = document.getElementById("scientific-department")
let literaryDepartment = document.getElementById("literary-department")
// this variables store select lists
let firstGradeSubjects = document.getElementById("first-grade-subjects");
let scienceStreamSubjects = document.getElementById("science-stream-subjects");
let artsStreamSubjects = document.getElementById("arts-stream-subjects");

primaryStage.addEventListener('click', function (){
  primaryMiddleSubjects.classList.remove("hidden");
  firstGradeSubjects.classList.add("hidden");
  scienceStreamSubjects.classList.add("hidden");
  artsStreamSubjects.classList.add("hidden");
})

middleStage.addEventListener('click', function (){
  primaryMiddleSubjects.classList.remove("hidden");
  firstGradeSubjects.classList.add("hidden");
  scienceStreamSubjects.classList.add("hidden");
  artsStreamSubjects.classList.add("hidden");
})

firstHighStage.addEventListener('click', function (){
  primaryMiddleSubjects.classList.add("hidden");
  firstGradeSubjects.classList.remove("hidden");
  scienceStreamSubjects.classList.add("hidden");
  artsStreamSubjects.classList.add("hidden");
})

scientificDepartment.addEventListener('click', function (){
  primaryMiddleSubjects.classList.add("hidden");
  firstGradeSubjects.classList.add("hidden");
  scienceStreamSubjects.classList.remove("hidden");
  artsStreamSubjects.classList.add("hidden");
})

literaryDepartment.addEventListener('click', function (){
  primaryMiddleSubjects.classList.add("hidden");
  firstGradeSubjects.classList.add("hidden");
  scienceStreamSubjects.classList.add("hidden");
  artsStreamSubjects.classList.remove("hidden");
})
// this part to add images and give apreview
// Select the upload button, input element, and preview container
const uploadButton = document.querySelector('button');
const uploadInput = document.getElementById('image-upload');
const imagePreviewContainer = document.querySelector('.image-preview-container');

// Set a maximum number of images allowed
const maxImages = 20;

// Add event listener to the upload input
uploadInput.addEventListener('change', function() {
  // Check if files are selected
  if (this.files) {
    // Limit the number of selected files to maxImages
    const selectedFiles = [...this.files].slice(0, maxImages);

    // Clear any existing previews
    imagePreviewContainer.innerHTML = '';

    // Loop through each selected file
    selectedFiles.forEach((file) => {
      // Create a new FileReader object
      const reader = new FileReader();

      // Add event listener to the reader object
      reader.addEventListener('load', function (e) {
        // Create an image element
        const image = new Image();
        image.src = e.target.result;

        // Create a new preview section element
        const previewSection = document.createElement('div');
        previewSection.classList.add('image-preview');
        previewSection.style.backgroundImage = `url(${image.src})`;
        previewSection.style.backgroundSize = 'contain';
        previewSection.style.backgroundPosition = 'center';

        // Add the preview section to the container
        imagePreviewContainer.appendChild(previewSection);
      });

      // Read the uploaded image as a data URL
      reader.readAsDataURL(file);
    });

    // Enable the upload button if files are selected
    if (selectedFiles.length > 0) {
      uploadButton.disabled = false;
      uploadButton.style.opacity = 1;
    } else {
      uploadButton.disabled = true;
      uploadButton.style.opacity = 0.5;
    }
  }
});
