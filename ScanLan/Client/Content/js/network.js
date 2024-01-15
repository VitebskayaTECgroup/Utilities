let card = document.getElementById('card')
let overlay = document.getElementById('overlay')

function _submit(ip) {

    let fields = document.querySelectorAll('textarea')

    let body = new FormData()
    body.append('Ip', ip)
    fields.forEach(field => body.append(field.name, field.value))

    fetch(host + 'network/submit', { method: 'POST', body })
        .then(res => res.text())
        .then(text => {
            if (text == 'success') {
                location.reload()
            } else {
                alert('error')
                console.error(text.substring(6))
            }
        })
}

function _open(ip) {
    fetch(host + 'network/card?ip=' + ip)
        .then(res => res.text())
        .then(text => {
            card.innerHTML = text
            if (card.classList.contains('hide')) {
                card.classList.remove('hide')
                overlay.classList.remove('hide')
            }
        })
}

function _close() {
    if (!card.classList.contains('hide')) {
        card.classList.add('hide')
        overlay.classList.add('hide')
    }
}