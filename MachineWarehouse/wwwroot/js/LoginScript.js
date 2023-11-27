const loginForm = document.querySelector('#AuthForm');

loginForm.addEventListener('submit', async e => {
    e.preventDefault();

    const response = await fetch(loginForm.action, {
        method: loginForm.method,
        body: new FormData(loginForm)
    });

    var data = await response.json();

    if (data.statusCode === 400) {
        return window.location.href = window.location.origin + '/api/Account/Registration';
    }

    localStorage.setItem('role', data.value.role);
    localStorage.setItem("accessToken", data.value.accessToken);
    accessKey = localStorage.getItem("accessToken");
    document.cookie = `accessToken=${accessKey}` + '; path=/';

    return window.location.href = window.location.origin + '/api/Car/Index';
});
