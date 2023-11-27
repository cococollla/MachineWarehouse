const loginForm = document.querySelector('#AuthForm');

loginForm.addEventListener('submit', async e => {
    e.preventDefault();

    const response = await fetch(loginForm.action, {
        method: loginForm.method,
        body: new FormData(loginForm)
    });

    fetch("api/Car/Index", {
        headers: {
            'Authorization': 'Basic dXNlcjpwd2Q=',
        },
    })

    var data = await response.json();

    if (data.statusCode === 400) {
        return window.location.href = window.location.origin + '/api/Account/Registration';
    }
    console.log(data);

    localStorage.setItem('role', data.value.role);
    localStorage.setItem("accessToken", data.value.accessToken);

    role = localStorage.getItem("role");
    accessKey = localStorage.getItem("accessToken");

    document.cookie = `role=${role}` + '; path=/';
    document.cookie = `accessToken=${accessKey}` + '; path=/';

    return window.location.href = window.location.origin + '/api/Car/Index';
});
