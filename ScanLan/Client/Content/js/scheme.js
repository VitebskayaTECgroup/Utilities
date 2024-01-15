document.querySelectorAll('g').forEach(g => {

    let links = document.querySelectorAll('line[class*="' + g.getAttribute('class') + '"]')
    console.log(links)

    g.onmouseenter = function () {
        links.forEach(link => link.classList.add('active'))
    }

    g.onmouseleave = function () {
        links.forEach(link => link.classList.remove('active'))
    }
})