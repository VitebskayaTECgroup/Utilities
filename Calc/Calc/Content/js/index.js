window.compute = function () {
	let recompute = false
	document.querySelectorAll('.sheet input').forEach(input => {
		let id = input.getAttribute('data-id')
		if (!id) return

		let evalue = 0
		try { evalue = ОКРУГЛ(verify(eval(input.getAttribute('ondrag'))), 4) } catch (e) {
			evalue =  0;
			console.error('Ошибка в формуле: ', input.getAttribute('ondrag'), ', ячейка ', id)
		}
		let value = verify(input.value)
		input.value = window[id] = evalue

		if (value != evalue) {
			console.log(id, ': ', value, ' => ', evalue)
			recompute = true
			if (input.hasAttribute('data-name')) {
				input.classList.add('uncommited')
				input.closest('tr').classList.add('uncommited')
			}
		}
	})
	if (recompute) return compute()
	else return commit()
}

window.commit = function () {
	document.querySelectorAll('tr.uncommited').forEach(row => {
		var date = row.querySelector('.date').innerHTML

		var form = new FormData()
		form.append('date', date)
		form.append('page', location.pathname)
		row.querySelectorAll('input').forEach(input => {
			if (!input.classList.contains('uncommited')) return
			if (!input.hasAttribute('data-name')) return
			let id = input.getAttribute('data-id')
			form.append(input.getAttribute('data-name'), window[id])
		})

		fetch(committer + 'default/commit', { method: 'POST', body: form })
			.then(res => res.text())
			.then(text => console.log(text))
	})
}

window.onload = function () {
	document.querySelectorAll('input[data-id]').forEach(input => {

		// Подготовка формул и глобальных переменных
		let id = input.getAttribute('data-id')
		let evl = input.getAttribute('ondrag') || id
		let value = input.value == '' ? null : verify(input.value)
		window[id] = window[id.toLowerCase()] = input.value = value
		input.setAttribute('ondrag', evl)

		// Определение обработчиков ввода данных
		input.addEventListener('focus', () => {
			value = verify(input.value)
		})
		input.addEventListener('blur', () => {
			var nvalue = verify(input.value)
			if (value != nvalue) {
				console.log(id, ': ', value, ' => ', nvalue)
				window[id] = window[id.toLowerCase()] = input.value = value = nvalue

				if (input.hasAttribute('data-name')) {
					input.classList.add('uncommited')
					input.closest('tr').classList.add('uncommited')
				}
				compute()
			}
		})
		input.addEventListener('keyup', e => {
			if (e.keyCode == 13) return input.blur()
			if (e.keyCode == 27) input.value = value
		})
	})

	compute()
}

document.addEventListener('contextmenu', function (e) {
	if (e.target.tagName == 'INPUT') {
		e.preventDefault()
		var menu = document.getElementById('menu')
		if (menu) menu.parentNode.removeChild(menu)
		menu = document.createElement('div')
		menu.id = 'menu'

		if (e.target.hasAttribute('ondrag')) {
			var span = document.createElement('span')
			span.innerHTML = 'Формула:<br />' + e.target.getAttribute('ondrag')
			menu.appendChild(span)
		}

		var span = document.createElement('span')
		span.innerHTML = 'Пересчитать'
		span.onclick = function () {
			var form = new FormData()
			form.append('date', e.target.parentNode.parentNode.querySelector('.date').innerHTML)
			form.append('page', location.pathname)
			form.append(e.target.getAttribute('data-name'), window[e.target.getAttribute('data-id')])

			fetch(committer + 'default/erase', { method: 'POST', body: form })
				.then(res => res.text())
				.then(text => location.reload())
		}
		menu.appendChild(span)

		var coords = e.target.getBoundingClientRect()
		menu.style.top = coords.top + window.pageYOffset + 25 + 'px'
		menu.style.left = coords.left + window.pageXOffset + 20 + 'px'
		document.body.appendChild(menu)
	}
})

document.addEventListener('click', function (e) {
	var menu = document.getElementById('menu')
	if (menu) {
		if (!e.target.closest('#menu')) {
			menu.parentNode.removeChild(menu)
		}
	}
})


var row = ''

document.querySelectorAll('[row]').forEach(function (el) {
	el.addEventListener('mouseenter', function (e) {
		var _row = e.target.getAttribute('row')
		if (_row == row) return
		row = _row
		document.querySelectorAll('[row="' + row + '"]').forEach(x => x.classList.add('hover'))
	})

	el.addEventListener('mouseleave', function (e) {
		row = ''
		document.querySelectorAll('.hover').forEach(x => x.classList.remove('hover'))
	})
})

function verify(number) {
	//if (number == null || number == '') return ''
	let n = parseFloat(('' + number).replace(',', '.'))
	return isNaN(n) ? 0 : n
}

function show(name) {
	document.querySelectorAll('.modal').forEach(el => el.style.display = el.id == name ? 'block' : 'none')
	document.getElementById('modal').style.display = 'block'
}

function closeModal() {
	document.querySelectorAll('.modal').forEach(el => el.style.display = 'none')
	document.getElementById('modal').style.display = 'none'
}

function fromASU(url) {
	var form = new FormData()
	var div = document.getElementById('import')
	div.querySelectorAll('input').forEach(el => form.append(el.name, el.value))

	fetch(url, { method: 'POST', body: form })
		.then(res => res.text())
		.then(text => {
			console.log(text)
			let data = text.split(':')
			switch (data[0]) {
				case 'error':
					alert('Ошибка! ' + data[1])
					break
				default:
					alert(data[1])
					location.reload()
					break
			}
		})
}

function R(number, digits) {
	let n = '1'
	digits = digits || 0
	try { while (digits--) n += '0' } catch (e) {}
	n = +n
	return Math.round(number * n) / n
}

function ОКРУГЛ(number, digits) {
	return R(number, digits)
}

function СУММ(letter) {
	let i = 1
	let summ = 0

	console.log(letter + i, window[letter + i])
	while (window[letter + i] !== undefined) {
		summ += Number(window[letter + i])
		i++
	}
	return summ
}

function СРЗНАЧ(letter, days) {
	return СУММ(letter) / days
}

function ЕСЛИ(expression, _then, _else) {
	return expression ? _then : _else
}

function ЕСЛИНЕТ(value, evl) {
	if (value == 0) return 0
	return (value && value != '' && value != null) ? value : evl
}

function ДЕЛ(a1, a2) {
	try {
		return a1 / a2
	}
	catch (e) {
		return 0
	}
}

function Коэффициент(значение, давление, температура, темпХолИст, константа, лист) {

	if (значение == 0 || давление == 0 || температура == 0) return 0

	try {

		let y = 0, x = 0, yNext = 0, xNext = 0;
		let koef = Коэф[лист]

		for (let key in koef) {
			if (температура >= key) y = key
			else {
				yNext = key
				break
			}
		}

		for (let key in koef[y]) {
			if (давление >= key) x = key
			else {
				xNext = key
				break
			}
		}

		let r = 0, r_lo = 0, r_hi = 0

		if (давление != x) {
			r_lo = x + ((давление - koef[y][x] / (koef[y][xNext] - koef[y][x])) * (koef[y][xNext] - koef[y][x]))
		} else {
			r_lo = koef[y][x]
		}

		if (температура != y) {
			if (давление != x) {
				r_hi = koef[yNext][x] + ((давление - x) / (xNext - x) * (koef[yNext][xNext] - koef[yNext][x]))
			} else {
				r_hi = koef[yNext][x]
			}
			r = r_lo + ((температура - y) / (yNext - y)) * (r_hi - r_lo)
		} else {
			r = r_lo
		}
		if (r == темпХолИст) return 0

		return Math.round(значение * Math.pow(константа / (r - темпХолИст), 2))
	}
	catch (e) {
		return 0
	}
}

function Коэффициент2(значение, давление, температура, темпХолИст, константа, лист) {
	return Коэффициент(значение, давление, температура, темпХолИст, константа, лист)
}

function СредневзвешенноеДавление(Значение, СмещениеПараметра) {
	try {
		p = 0
		result = 0

		let i = 1;
		while (window[Значение + i] != undefined) {
			let ddd = window[СмещениеПараметра + i]
			result += window[Значение + i] * ddd
			p = p + ddd
			i++
		}

		if (p == 0) {
			result = 0
		} else {
			result = result / p
		}
		return result
	}
	catch (e) {
		return 0
	}
}

const Коэф = {
	КоэфРивкин: {
		40: {
			40: 0.001,
			85: 0,
		},
		120: {
			40: 0.0011,
			85: 0,
		},
		140: {
			40: 0.0011,
			85: 0,
			120: 0.0011,
			161: 0,
		},
		170: {
			120: 0.0011,
			161: 0,
		},
		210: {
			120: 0.0012,
			161: 0,
		},
		250: {
			6.5: 0.3702,
			7: 0.3432,
			7.5: 0.3197,
			8: 0.2991,
			8.5: 0.281,
			9: 0.2649,
			9.5: 0.2505,
			10: 0.2375,
			26: 0,
		},
		260: {
			6.5: 0.375,
			7: 0.3504,
			7.5: 0.3265,
			8: 0.3055,
			8.5: 0.287,
			9: 0.2706,
			9.5: 0.2559,
			10: 0.2427,
			26: 0,
		},
		270: {
			6.5: 0.3856,
			7: 0.3575,
			7.5: 0.3332,
			8: 0.3119,
			8.5: 0.293,
			9: 0.2763,
			9.5: 0.2614,
			10: 0.2479,
			26: 0,
		},
		280: {
			6.5: 0.3933,
			7: 0.3647,
			7.5: 0.3399,
			8: 0.3182,
			8.5: 0.299,
			9: 0.282,
			9.5: 0.2667,
			10: 0.253,
			26: 0,
		},
		290: {
			6.5: 0.4009,
			7: 0.3718,
			7.5: 0.3465,
			8: 0.3244,
			8.5: 0.3049,
			9: 0.2876,
			9.5: 0.2721,
			10: 0.2581,
			26: 0,
		},
		300: {
			6.5: 0.4085,
			7: 0.3788,
			7.5: 0.3531,
			8: 0.3307,
			8.5: 0.3108,
			9: 0.2932,
			9.5: 0.2774,
			10: 0.2632,
			26: 0,
		},
		310: {
			6.5: 0.416,
			7: 0.3859,
			7.5: 0.3597,
			8: 0.3369,
			8.5: 0.3167,
			9: 0.2987,
			9.5: 0.2827,
			10: 0.2682,
			27: 0.0951,
			28: 0.09145,
			29: 0.08805,
			30: 0.08488,
			31: 0.08191,
			32: 0.07912,
			33: 0.0765,
			34: 0.07403,
			35: 0.0717,
			36: 0.0695,
			37: 0.06742,
			38: 0.6545,
			39: 0,
		},
		320: {
			6.5: 0.4236,
			7: 0.3929,
			7.5: 0.3663,
			8: 0.343,
			8.5: 0.3225,
			9: 0.3043,
			9.5: 0.2879,
			10: 0.2732,
			27: 0.0972,
			28: 0.0935,
			29: 0.09,
			30: 0.0868,
			31: 0.0838,
			32: 0.081,
			33: 0.0783,
			34: 0.0758,
			35: 0.0734,
			36: 0.0712,
			37: 0.6908,
			38: 0.0671,
			39: 0,
		},
		330: {
			6.5: 0.4311,
			7: 0.3999,
			7.5: 0.3729,
			8: 0.3492,
			8.5: 0.3283,
			9: 0.3098,
			9.5: 0.2932,
			10: 0.2782,
			27: 0.0993,
			28: 0.0955,
			29: 0.092,
			30: 0.0887,
			31: 0.0856,
			32: 0.0828,
			33: 0.0801,
			34: 0.0775,
			35: 0.0751,
			36: 0.0729,
			37: 0.0707,
			38: 0.0687,
			39: 0,
		},
		340: {
			6.5: 0.4386,
			7: 0.4069,
			7.5: 0.3794,
			8: 0.3553,
			8.5: 0.3341,
			9: 0.3153,
			9.5: 0.2984,
			10: 0.2832,
			27: 0.103,
			28: 0.0975,
			29: 0.0939,
			30: 0.0906,
			31: 0.0875,
			32: 0.0845,
			33: 0.0818,
			34: 0.0792,
			35: 0.0768,
			36: 0.0745,
			37: 0.0723,
			38: 0.0702,
			39: 0,
		},
		350: {
			6.5: 0.4461,
			7: 0.4138,
			7.5: 0.3859,
			8: 0.3615,
			8.5: 0.3399,
			9: 0.3207,
			9.5: 0.3036,
			10: 0.2881,
			27: 0.1033,
			28: 0.0994,
			29: 0.0958,
			30: 0.0924,
			31: 0.0893,
			32: 0.0863,
			33: 0.0835,
			34: 0.0809,
			35: 0.0784,
			36: 0.0761,
			37: 0.0739,
			38: 0.0718,
			39: 0,
		},
		360: {
			6.5: 0.4535,
			7: 0.4208,
			7.5: 0.3924,
			8: 0.3676,
			8.5: 0.3457,
			9: 0.3262,
			9.5: 0.3088,
			10: 0.2931,
			27: 0.1053,
			28: 0.1014,
			29: 0.0977,
			30: 0.0943,
			31: 0.091,
			32: 0.088,
			33: 0.0852,
			34: 0.0825,
			35: 0.08,
			36: 0.0776,
			37: 0.0754,
			38: 0.0733,
			39: 0,
		},
		370: {
			6.5: 0.461,
			7: 0.4277,
			7.5: 0.3989,
			8: 0.3737,
			8.5: 0.3514,
			9: 0.3316,
			9.5: 0.3139,
			10: 0.298,
			27: 0.1073,
			28: 0.1033,
			29: 0.0995,
			30: 0.0961,
			31: 0.0919,
			32: 0.0897,
			33: 0.0869,
			34: 0.08415,
			35: 0.0816,
			36: 0.0792,
			37: 0.0769,
			38: 0.0748,
			39: 0,
		},
		380: {
			6.5: 0.4684,
			7: 0.4347,
			7.5: 0.4054,
			8: 0.3798,
			8.5: 0.3572,
			9: 0.3371,
			9.5: 0.319,
			10: 0.3029,
			27: 0.1092,
			28: 0.1052,
			29: 0.1014,
			30: 0.0987,
			31: 0.0928,
			32: 0.0914,
			33: 0.0885,
			34: 0.0858,
			35: 0.0832,
			36: 0.0807,
			37: 0.0784,
			38: 0.0762,
			39: 0,
		},
		490: {
			86: 0.0388,
			88: 0.0378,
			90: 0.0369,
			92: 0.036,
			94: 0.0352,
			96: 0.0344,
			98: 0.0336,
			100: 0.0329,
			119: 0,
		},
		500: {
			86: 0.0394,
			88: 0.0385,
			90: 0.0375,
			92: 0.0367,
			94: 0.0358,
			96: 0.035,
			98: 0.0342,
			100: 0.0335,
			119: 0,
		},
		510: {
			86: 0.0401,
			88: 0.0391,
			90: 0.0382,
			92: 0.0367,
			94: 0.0364,
			96: 0.035,
			98: 0.0348,
			100: 0.0341,
			119: 0,
		},
		520: {
			86: 0.0407,
			88: 0.0398,
			90: 0.0388,
			92: 0.0373,
			94: 0.037,
			96: 0.0362,
			98: 0.0354,
			100: 0.0346,
			119: 0,
		},
		530: {
			86: 0.0414,
			88: 0.0404,
			90: 0.0394,
			92: 0.0379,
			94: 0.0376,
			96: 0.0368,
			98: 0.036,
			100: 0.0352,
			119: 0,
		},
		540: {
			86: 0.042,
			88: 0.041,
			90: 0.0401,
			92: 0.0385,
			94: 0.0382,
			96: 0.0374,
			98: 0.0366,
			100: 0.0358,
			119: 0,
		},
		550: {
			86: 0.0427,
			88: 0.0417,
			90: 0.0407,
			92: 0.0391,
			94: 0.0388,
			96: 0.038,
			98: 0.0372,
			100: 0.0364,
			119: 0,
		},
	},
	КоэфAlpia: {
		175: {
			0.010133: 674.835,
			0.5: 668.936,
			1: 177.056,
			2.5: 177.248,
			5: 177.558,
		},
		200: {
			0.010133: 686.634,
			0.5: 681.881,
			1: 675.313,
			2.5: 203.688,
			5: 203.927,
		},
		250: {
			0.010133: 710.28,
			0.5: 707.008,
			1: 702.661,
			2.5: 687.637,
			5: 259.219,
		},
		300: {
			0.010133: 734.188,
			0.5: 731.752,
			1: 728.623,
			2.5: 718.448,
			5: 698.385,
		},
		350: {
			0.010133: 758.383,
			0.5: 756.423,
			1: 754.036,
			2.5: 746.632,
			5: 732.779,
		},
	}
}