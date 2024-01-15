function cookie(name, value, options) {
	if (!value) {
		var matches = document.cookie.match(new RegExp("(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"))
		return matches ? decodeURIComponent(matches[1]) : undefined
	}
	else {
		options = options || {}
		var expires = options.expires
		if (typeof expires == "number" && expires) {
			var d = new Date()
			d.setTime(d.getTime() + expires * 1000)
			expires = options.expires = d
		}
		if (expires && expires.toUTCString) options.expires = expires.toUTCString()
		value = encodeURIComponent(value)
		var updatedCookie = name + "=" + value
		for (var propName in options) {
			updatedCookie += "; " + propName
			var propValue = options[propName]
			if (propValue !== true) updatedCookie += "=" + propValue
		}
		document.cookie = updatedCookie
		return value
	}
}

function dateToString(date, format) {
	format = format || 'dd.MM.yyyy';
	date = date || new Date();
	if (!isValidDate(date)) return 'Invalid Date';
	return format
		.replace('yyyy', '' + date.getFullYear())
		.replace('MM', to2(date.getMonth() + 1))
		.replace('dd', to2(date.getDate()))
		.replace('hh', to2(date.getHours()))
		.replace('mm', to2(date.getMinutes()));
	function to2(n) { return (n < 10 ? '0' : '') + n; }
}

function stringToDate(str, format) {
	str = str || ''
	format = format || 'dd.MM.yyyy hh:mm:ss'

	var date = new Date(sub('yyyy'), sub('MM') - 1, sub('dd'), sub('hh'), sub('mm'), sub('ss'))
	if (isValidDate(date)) return date

	function sub(token) {
		var ch = 0
		if ((ch = format.indexOf(token)) > -1) {
			var n = Number(str.slice(ch, ch + token.length))
			return isNaN(n) ? 0 : n
		} else return 0
	}
}

function isValidDate(date) {
	return date instanceof Date && !isNaN(date.getTime())
}

function sort(th) {

	var tr = th.parentNode
	var table = tr.parentNode.parentNode
	var tbody = table.getElementsByTagName('tbody')[0]
	var rows = tbody.getElementsByTagName('tr')
	var arr = []
	var way = th.getAttribute('sort-way')
	var type = th.getAttribute('sort-type')
	var compare
	var colNum = th.cellIndex

	for (var i = 0; i < rows.length; i++) arr.push(rows[i])

	switch (type) {

		case 'number':
			compare = function (a, b) {
				return way === 'up'
					? Number(a.cells[colNum].innerHTML.replace(',', '.')) - Number(b.cells[colNum].innerHTML.replace(',', '.'))
					: Number(b.cells[colNum].innerHTML.replace(',', '.')) - Number(a.cells[colNum].innerHTML.replace(',', '.'))
			}
			break

		case 'date':
			compare = function (a, b) {
				var A = +stringToDate(a.cells[colNum].innerHTML, 'hh:mm:ss')
				var B = +stringToDate(b.cells[colNum].innerHTML, 'hh:mm:ss')
				return way === 'up'
					? B > A ? 1 : -1
					: A > B ? 1 : -1
			}
			break

		default:
			compare = function (a, b) {
				return way === 'up'
					? a.cells[colNum].innerHTML > b.cells[colNum].innerHTML ? 1 : -1
					: b.cells[colNum].innerHTML > a.cells[colNum].innerHTML ? 1 : -1
			}
			break
	}

	for (var i = 0; i < tr.cells.length; i++) tr.cells[i].className = ''

	if (way === 'up') {
		th.setAttribute('sort-way', 'down');
		th.className = 'sort_down';
	} else {
		th.setAttribute('sort-way', 'up');
		th.className = 'sort_up';
	}

	arr.sort(compare)

	table.removeChild(tbody)
	tbody = document.createElement('tbody')
	for (var i = 0; i < arr.length; i++) tbody.appendChild(arr[i])
	table.appendChild(tbody)
}

if (window.NodeList && !NodeList.prototype.forEach) {
	NodeList.prototype.forEach = function (callback, thisArg) {
		thisArg = thisArg || window;
		for (var i = 0; i < this.length; i++) {
			callback.call(thisArg, this[i], i, this);
		}
	};
}


var cardId = 0

function searchKeyPress() {
	var e = event || window.event
	if (e.keyCode == 13) load()
}

function card(id, date) {
	$.get(host + 'home/card/' + id, { date: date })
		.fail(function () { })
		.done(function (data) {
			changeCardView(true)
			document.getElementById('card').innerHTML = data
			cardId = id
			$('#date-card').datepicker('setDate', stringToDate(date))
			events()
		})
}

function load() {

	$('#view-content').html('Запрос данных...')

	var filter = getFilter()
	lastDate = new Date().toISOString()

	var mode = document.getElementById('mode').value

	cookie('checkpoint-mode', mode)
	cookie('checkpoint-department', filter.department)
	cookie('checkpoint-group', filter.group)
	cookie('checkpoint-onlyBad', filter.onlyBad)
	cookie('checkpoint-onlyUnseen', filter.onlyUnseen)
	cookie('checkpoint-onlyRefused', filter.onlyRefused)
	cookie('checkpoint-type', filter.type)

	$.get(host + 'home/' + mode, filter)
		.fail(function () {
			$('#view-content').html('Произошла ошибка при обращении к серверу')
		})
		.done(function (data) {
			$('#view-content').html(data)
		})
}

function print() {
	var query = getFilter()

	$.post(host + 'print/table', query)
		.fail(function () {
			alert('Ошибка при печати. Обратитесь в уАСУТП по тел. 3-32')
		})
		.done(function (data) {
			var a = document.createElement('a')
			a.download = data.replace('/content/docs/', '').replace('/checkpoint', '')
			a.href = data
			a.click()
		})
}

var lastDate = ''

function updateTimeline() {
	var filter = getFilter()
	lastDate = new Date().toISOString()

	var mode = document.getElementById('mode').value

	cookie('checkpoint-mode', mode)
	cookie('checkpoint-department', filter.department)
	cookie('checkpoint-group', filter.group)
	cookie('checkpoint-onlyBad', filter.onlyBad)
	cookie('checkpoint-onlyUnseen', filter.onlyUnseen)
	cookie('checkpoint-onlyRefused', filter.onlyRefused)
	cookie('checkpoint-type', filter.type)

	$.get(host + 'home/' + mode + 'update', filter)
		.fail(function () {
			//$('#view-content').html('Произошла ошибка при обращении к серверу')
		})
		.done(function (data) {
			document.getElementById('view-content').querySelector('tbody').insertAdjacentHTML('afterbegin', data)
		})
}

function getFilter() {
	return {
		search: document.getElementById('search').value,
		last: lastDate,
		date: dateToString($('#date').datepicker('getDate')),
		department: document.getElementById('department').value,
		type: document.getElementById('type').value,
		group: document.getElementById('group').value,
		onlyBad: document.getElementById('onlyBad').checked.toString(),
		onlyUnseen: document.getElementById('onlyUnseen').checked.toString(),
		onlyRefused: document.getElementById('onlyRefused').checked.toString()
	}
}

function events() {

	closeBigPhoto()

	var filter = {
		date: dateToString($('#date-card').datepicker('getDate')),
		mode: document.getElementById('mode-card').value
	}

	$('#events').html('Запрос данных...')

	$.get(host + 'home/events/' + cardId, filter)
		.fail(function () { })
		.done(function (data) {
			$('#events').html(data)
		})
}

function setFilter(mode) {
	console.log(mode)
	document.getElementById('mode-' + (mode === 'list' ? 'timeline' : 'list')).style.display = 'none'
	document.getElementById('mode-' + mode).style.display = 'inherit'
}

function changeCardView(setVisible) {
	closeBigPhoto()
	document.getElementById('card').style.display = document.getElementById('filter-card').style.display = setVisible ? '' : 'none'
	document.getElementById('view').style.display = document.getElementById('filter-view').style.display = setVisible ? 'none' : ''
}

function showBigPhoto(img) {
	document.getElementById('card').style.display = 'none'
	document.getElementById('big-photo-button').style.display = ''
	var big = document.getElementById('big-photo')
	big.querySelector('img').src = img.src
	big.style.display = ''
}

function closeBigPhoto() {
	document.getElementById('big-photo-button').style.display = 'none'
	document.getElementById('big-photo').style.display = 'none'
	document.getElementById('card').style.display = ''
}

function printExcel() {
	var query = {
		date: dateToString($('#date-card').datepicker('getDate')),
		mode: $('#mode-card').val()
	}

	$.get(host + 'print/index/' + cardId, query)
		.fail(function (xhr) { })
		.done(function (data) {
			var a = document.createElement('a')
			a.setAttribute('download', '')
			a.href = data
			document.body.appendChild(a)
			a.click()
		})
}

$(function () {

	$('#date').datepicker({
		dateFormat: 'dd.mm.yy',
		monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
		dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
		firstDay: 1,
		maxDate: 0,
		onSelect: load
	});

	$('#date-card').datepicker({
		dateFormat: 'dd.mm.yy',
		monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
		dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
		firstDay: 1,
		maxDate: 0,
		onSelect: events
	});

	//document.getElementById('mode').onchange = function () {
	//    setFilter(this.value)
	//    load()
	//}

	$('#view-content').on('click', 'th', function () {
		sort(this)
	})

	load()
})