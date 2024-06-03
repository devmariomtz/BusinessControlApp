document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('darkModeSwitch').addEventListener('change', () => {
        if (document.documentElement.getAttribute('data-bs-theme') == 'dark') {
            document.documentElement.setAttribute('data-bs-theme', 'light')
            // guardamos el valor en localStorage
            localStorage.setItem('theme', 'light');
        }
        else {
            document.documentElement.setAttribute('data-bs-theme', 'dark')
            // guardamos el valor en localStorage
            localStorage.setItem('theme', 'dark');
        }
    });

    // recuperamos el valor de localStorage
    const theme = localStorage.getItem('theme');
    if (theme) {
        document.documentElement.setAttribute('data-bs-theme', theme);
        if (theme === 'dark') {
            document.getElementById('darkModeSwitch').checked = true;
        }
    }
});



