var tokenKey = "accessToken";

document.getElementById("submitLogin").addEventListener("click", async e => {
    e.preventDefault();

    const formData = new FormData();
    formData.append("name", document.getElementById("name").value);
    formData.append("password", document.getElementById("password").value);
    formData.append("login", document.getElementById("login").value);
    formData.append("email", document.getElementById("email").value);

    const response = await fetch("Authenticate", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: formData
    });
    var data = await response.json();
    console.log(response);
    console.log(data.value.accessToken);

    if (response.ok === true) {
        var redirectUrl = "https://localhost:7001/api/Car/Index";

        localStorage.setItem(tokenKey, data.value.accessToken);
        var token = localStorage.getItem(tokenKey);
        console.log(token);
        var accessToken = "Bearer " + token;
        document.cookie = `accessToken=${token}`;
        localStorage.setItem('role', data.value.role);
        //console.log(accessToken);
        //window.location.href = redirectUrl;
        const response1 = await fetch(redirectUrl, {
            method: 'GET',
            headers: {
                'Authorization': accessToken,
                'Content-Type': 'application/json'
            }
        });

        var data1 = response1.json();        
        console.log(data1);
        console.log("bye");
    }
});

//document.addEventListener('DOMContentLoaded', function () {
//    const form = document.querySelector('#AuthForm');

//    form.addEventListener('submit', function () {
//        fetch(form.action, {
//            method: form.method,
//            body: new FormData(form)
//        })
//            .then(response => {
//                if (response.ok) {
//                    return response.json();
//                } else {
//                    throw new Error('Ошибка при отправке формы');
//                }
//            })
//            .then(data => {
//                if (data != '') {
//                    document.cookie = `accessToken=${data.Token}`;
//                    localStorage.setItem('role', data.Role);
//                    console.log(data.Token);
//                    window.location.href = "https://localhost:7001/api/Car/Index";
//                } else {
//                    throw new Error('Токен не найден в ответе сервера');
//                }
//            })
//            .catch(error => {
//                console.error('Произошла ошибка:', error);
//            });
//    });
//});


