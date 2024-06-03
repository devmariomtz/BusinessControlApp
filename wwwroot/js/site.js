document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('darkModeSwitch').addEventListener('change', () => {
        if (document.documentElement.getAttribute('data-bs-theme') == 'dark') {
            document.documentElement.setAttribute('data-bs-theme', 'light')
        }
        else {
            document.documentElement.setAttribute('data-bs-theme', 'dark')
        }
    })
});



