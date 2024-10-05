// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    const selected_nav = window.location.href.split("/").pop();
    // const selectedHomeNavItems = ['Home']
    if (selected_nav === '') {
        $('a[data-navselect="Home"]').addClass("active-nav");
    }
    if (selected_nav == 'Privacy') {
        $('a[data-navselect="Privacy"]').addClass("active-nav");
    }
    if (selected_nav == 'CartList') {
        $('a[data-navselect="CartList"]').addClass("active-nav");
    }
    
    if (selected_nav == 'ManageProducts') {
        $('a[data-navselect="AdminProductList"]').addClass("active-nav");
    }
})