const channelTokenBroadcast = new BroadcastChannel('channelToken');
channelTokenBroadcast.onmessage = function (event) {
    localStorage.removeItem(event.data.item);
}

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
    var data = await response.text();
    var data = data.replace(new RegExp("\"", 'g'), "");
    console.log(response);
    console.log(data);

    if (response.ok === true) {
        var rediractUrl = "https://localhost:7001/api/Car/Index";
        var accessToken = "Bearer " + data.Token;
        //localStorage.setItem(tokenKey, accessToken);
        //channelTokenBroadcast.postMessage({ accessToken: accessToken });

        //window.location.href = "https://localhost:7001/api/Car/Index";

        fetch(rediractUrl,
            {
                headers: {
                    'Authorization': accessToken
                }
            })
            .then(() => {
                window.location.replace("https://localhost:7001/api/Car/Index");
        });

    }
});

//document.addEventListener('DOMContentLoaded', function () {
//    const form = document.querySelector('#AuthForm');

//    form.addEventListener('submit', function () {
//        fetch("api/Account/Token", {
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
//                    sessionStorage.setItem("Token", data.Token);
//                    console.log(data.Token);
//                    window.location.href = "/Read/Index";
//                } else {
//                    throw new Error('Токен не найден в ответе сервера');
//                }
//            })
//            .catch(error => {
//                console.error('Произошла ошибка:', error);
//            });
//    });
//});

//async function getData(url) {
//    const token = sessionStorage.getItem("Token");

//    const response = await fetch(url, {
//        method: "GET",
//        headers: {
//            "Accept": "application/json",
//            "Authorization": "Bearer " + token  // передача токена в заголовке
//        }
//    });
//    if (response.ok === true) {

//        const data = await response.json();
//        alert(data)
//    }
//    else
//        console.log("Status: ", response.status);
//};

