// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('darkModeSwitch').addEventListener('change', () => {
        console.log('entra en el event listener');
        console.log("data-bs-theme", document.documentElement.getAttribute('data-bs-theme'));
        if (document.documentElement.getAttribute('data-bs-theme') == 'dark') {
            document.documentElement.setAttribute('data-bs-theme', 'light')
        }
        else {
            document.documentElement.setAttribute('data-bs-theme', 'dark')
        }
    })
});



