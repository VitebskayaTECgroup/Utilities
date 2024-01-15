var TimePicker = (function () {

	function TimePicker(options) {

		options = options || {}
		this.target = document.querySelector(options.el)
		if (!this.target) return console.error('Не передан родительский элемент')
	
		this.onSelect = options.onSelect || function () {}
		this.el = document.createElement('div')
		this.el.className = 'timepicker'
		document.body.appendChild(this.el)
	
		this.height = 0
		this.width = 0
		this.step = options.step || 15
		this.date = options.date || new Date(2000, 1, 1, 0, 0)
		this.viewDate = new Date(0, 0, 0, this.date.getHours(), this.date.getMinutes())
		this.format = options.format || 'hh:mm'
	
		var self = this
	
		// События целевого элемента
	
		addEvent(this.target, 'focus', function () {
			self.show()
		})
	
		addEvent(this.target, 'change', function () {
			var date = stringToDate(self.target.value, self.format)
			self.date = date || self.date
			self.target.value = dateToString(self.date, self.format)
			self.render()
		})
	
		addEvent(document, 'click', function (e) {
			e = e || window.event
			var src = e.target || e.srcElement
			var hide = true
			while (true) {
				if (src == self.el || src == self.target) {
					hide = false
					break
				}
				if (!src) hide = false
				if (!src || src.tagName == 'HTML') break
				else src = src.parentNode
			}
			if (hide) self.hide()
		})
	
		addEvent(window, 'resize', function () {
			if (self.el.style.display !== 'none') { self.position() }
		})
	
		// События при выборе времени в пикере
	
		addEvent(this.el, 'click', function (e) {
			e = e || window.event
			var src = e.target || e.srcElement
	
			var time = src.innerHTML
			self.date = stringToDate(time, self.format)
			self.target.value = dateToString(self.date, self.format)
			self.hide()
		})
	}
	
	TimePicker.prototype.render = function () {
	
		function to2(n) { return (n < 10 ? '0': '') + n }
	
		var html = ''
		var viewDate = new Date(0,0,0,0,0)
	
		for (var i = 0; i < 1440; i += this.step) {
	
			html += '<div' + (viewDate.getHours() == this.date.getHours() && viewDate.getMinutes() == this.date.getMinutes() ? ' class="picked"' : '') + '>' + to2(viewDate.getHours()) + ':' + to2(viewDate.getMinutes()) + '</div>'
			viewDate.setMinutes(viewDate.getMinutes() + this.step)
		}
	
		this.el.innerHTML = html
	
		var scrolled = this.el.querySelector('.picked')
		scrolled = scrolled.previousSibling || scrolled
		scrolled.scrollIntoView()
	}
	
	TimePicker.prototype.position = function () {
	
		if (this.height === 0 && this.width === 0) {
			this.el.style.cssText =
				'display: block;' +
				'position: absolute;' +
				'visibility: hidden;'
			var rect    = this.el.getBoundingClientRect()
			this.height = rect.bottom - rect.top
			this.width  = rect.right - rect.left
		}
	
		var rect = this.target.getBoundingClientRect()
		var h = window.innerHeight
			|| document.documentElement.clientHeight
			|| document.body.clientHeight
		var w = window.innerWidth
			|| document.documentElement.clientWidth
			|| document.body.clientWidth
		var top  = rect.bottom + 2
		var left = rect.left
		
		this.width = rect.right - rect.left
	
		if (left + this.width > w) left = (w - this.width - 5)
		if (top + this.height > h) top = (h - this.height - 5)
		if (rect.left <= 0) left = 5
		if (rect.top <= 0) top = 5
	
		this.el.style.cssText = 'position:absolute;display:block;top:' + top + 'px;left:' + left + 'px;width:' + this.width + 'px;'
	}
	
	TimePicker.prototype.show = function () {
		this.render()
		this.position()
	}
	
	TimePicker.prototype.hide = function () {
		this.el.style.display = 'none'
	}
	
	var
		listener = !!window.addEventListener,
	
		isValidDate = function (date) {
			return date instanceof Date && !isNaN(date.getTime())
		},
	
		stringToDate = function (str, format) {
			if (!str || str === '') return
	
			var date = new Date(sub('yyyy'), sub('MM') - 1, sub('dd'), sub('hh'), sub('mm'))
			if (isValidDate(date)) return date
	
			function sub(token) {
				var ch = 0
				if ((ch = format.indexOf(token)) > -1) {
					var n = Number(str.slice(ch, ch + token.length))
					return isNaN(n) ? 0 : n
				} else return 0
			}
		},
	
		dateToString = function (date, format) {
			if (!isValidDate(date)) return 'Invalid Date'
			return format
				.replace('yyyy', '' + date.getFullYear())
				.replace('MM', to2(date.getMonth() + 1))
				.replace('dd', to2(date.getDate()))
				.replace('hh', to2(date.getHours()))
				.replace('mm', to2(date.getMinutes()))
			function to2(n) { return (n < 10 ? '0' : '') + n }
		},
	
		addEvent = function (el, e, callback) {
			listener ? el.addEventListener(e, callback) : el.attachEvent('on' + e, callback)
		}

	return TimePicker
})()