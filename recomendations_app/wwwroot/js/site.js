storage = window.localStorage

const theme = () => {
    document.querySelector("body").setAttribute("data-bs-theme", "dark");
    document.querySelector("#dl-icon").setAttribute("class", "bi bi-sun-fill");
    storage.setItem('theme','dark');
}

const themeLight = () => {
    document.querySelector("body").setAttribute("data-bs-theme", "light");
    document.querySelector("#dl-icon").setAttribute("class", "bi bi-moon-fill");
    storage.setItem('theme', 'light');
}

const switchTheme = () => {
    if (document.querySelector("body").getAttribute("data-bs-theme") === "light") {
        theme();
        console.log("light");
    } else {
        themeLight();
        console.log("dark");
    }
}

document.addEventListener("DOMContentLoaded", function() {
    if (storage.getItem('theme') === "light") {
        themeLight();
    } else {
        theme();
    }
});