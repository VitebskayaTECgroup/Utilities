function send(url) {
    var form =
        'user=' + encodeURIComponent(document.getElementById('user').value) +
        '&id=' + encodeURIComponent(document.getElementById('id').value) +
        '&comment=' + encodeURIComponent(document.getElementById('comment').value)

    var xhr = new XMLHttpRequest()
    xhr.open('POST', url, true)
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded')
    xhr.onreadystatechange = function () {
        if (xhr.readyState != 4) return
        if (xhr.responseText != 'done') return alert('Произошла ошибка выполнения скрипта')
        alert('Заявка создана')
        back()
    }
    xhr.send(form)
}

function review(url) {
    var xhr = new XMLHttpRequest()
    xhr.open('GET', url, true)
    xhr.onreadystatechange = function () {
        if (xhr.readyState != 4) return
        if (xhr.responseText != 'done') return alert('Произошла ошибка выполнения скрипта')
        alert('Статус изменён')
        location.reload()
    }
    xhr.send()
}

function back() {
    location = '/site'
}