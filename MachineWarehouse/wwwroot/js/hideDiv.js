window.onload=function() {
    role = getCookie("role");
    if (role === "User") {
        document.getElementById("createCar").style.display = 'none';
        document.getElementById("checkUser").style.display = 'none';
        document.getElementById("editCar").style.display = 'none';
        document.getElementById("deleteCar").style.display = 'none';
    }

    if (role === "Manager") {
        document.getElementById("checkUser").style.display = 'none';
    }

    if (role === "Admin") {
        document.getElementById("editCar").style.display = 'none';
        document.getElementById("deleteCar").style.display = 'none';
        document.getElementById("createCar").style.display = 'none';
    }
}

function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}