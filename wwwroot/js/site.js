// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Confirm(itemId) {
    const button = document.getElementById(itemId);
    button.innerText = "Sure?";
    button.setAttribute("onclick", `Buy(${itemId})`);
    console.log("Confirmation triggered");
}

function Buy(itemId) {

    console.log("Bought");
}