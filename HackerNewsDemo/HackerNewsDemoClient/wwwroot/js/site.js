// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getBaseUrl() {
    return window.location.origin;
}

function navigateToNewStories() {
    location.href = getBaseUrl() + "/Home/NewStories";
}

function navigateToBestStories() {
    location.href = getBaseUrl() + "/Home/BestStories";
}

function navigateToTopStories() {
    location.href = getBaseUrl() + "/Home/TopStories";
}