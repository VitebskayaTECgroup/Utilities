$(function () {

    $('#card').on('contextmenu', 'td[id]', function (e) {
        e.preventDefault()

        var t = prompt('Время (пустое для удаления)', this.innerHTML)
        if (t == null) return

        $.post(host + 'fix/' + (t == '' ? 'delete/' : 'update/') + this.id, {
            date: this.parentNode.getAttribute('date') + ' ' + t
        })
            .fail(f)
            .done(events)
    })

    $('#card').on('contextmenu', 'img[id]', function (e) {
        e.preventDefault()

        var t = prompt('Новое время', this.innerHTML)
        if (t == null) return
        var type = Number(prompt('Тип (вход=1,выход=2)', this.innerHTML))
        if (isNaN(type) || (type != 1 && type != 2)) type = 1

        $.post(host + 'fix/create/' + this.id, {
            type: type,
            date: dateToString($('#date-card').datepicker('getDate')) + ' ' + t
        })
            .fail(f)
            .done(events)
    })

    function f(xhr) { alert('Fail: ' + xhr.status + ' [' + xhr.statusText + ']') }
})