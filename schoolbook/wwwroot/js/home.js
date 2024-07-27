// start header > header links 
window.onload = function() {
    let headerLinksButton = document.getElementById("header-links");
    let headerLinks = document.getElementById("links-list");

    headerLinksButton.onclick = function() {
        headerLinks.classList.toggle("hidden");  
    }
}
//end header > acount list
// start header > acount list 
let account = document.getElementById("account");
let accountLinks = document.getElementById("account-links");

account.onclick = function() {
    accountLinks.classList.toggle("hidden");  
}
//end header > acount list
// start landing page small images
let preview = document.getElementById("preview");
let defaulLanding = document.getElementById("land-img");
let landingOne = document.getElementById("land-img1");
let landingTwo = document.getElementById("land-img2");
let landingThree = document.getElementById("land-img3");
let landingFour = document.getElementById("land-img4");

landingOne.onclick = function () {
    preview.src = this.src;
}

landingTwo.onclick = function () {
    preview.src = this.src;
}

landingThree.onclick = function () {
    preview.src = this.src;
}

landingFour.onclick = function () {
    preview.src = this.src;
}

defaulLanding.onclick = function () {
    preview.src = this.src;
}
// end landing page small images