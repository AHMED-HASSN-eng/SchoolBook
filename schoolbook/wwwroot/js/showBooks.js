// filters button
let filtersButton = document.getElementById("filter-button");
let filters = document.querySelector(".books-filters");

filtersButton.onclick = function () {
    filters.classList.toggle("hidden");
}

// change background for filter-box when chosing it

let test = document.getElementById("test");
let chech = document.getElementById("chech");

chech.onclick = function () {
    chech.classList.toggle("test");
}







