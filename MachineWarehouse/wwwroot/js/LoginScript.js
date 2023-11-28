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

    document.cookie = `role=${data.value.role}` + '; path=/';
    document.cookie = `accessToken=${data.value.accessToken}` + '; path=/';

    return window.location.href = window.location.origin + '/api/Car/Index';
});
