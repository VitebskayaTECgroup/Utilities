'use strict'

var Sort = (function () {

	function Sort(theadEl, tableEl) {
		var thead = document.querySelector(theadEl)
		var headers = thead.getElementsByTagName('th')

		var table = document.querySelector(tableEl)
		var tbody = table.getElementsByTagName('tbody')[0]

		for (var i = 0; i < headers.length; i++) {
			headers[i].onclick = function () {

				var rowsArray = [],
					type = this.getAttribute('type') || 'string',
					way = this.getAttribute('way'),
					colNum = this.cellIndex,
					compare

				for (var i = 0; i < tbody.rows.length; i++) rowsArray.push(tbody.rows[i])

				switch (type) {

					case 'number':
						compare = function (rowA, rowB) {
							return way == 'up'
								? rowA.cells[colNum].innerHTML - rowB.cells[colNum].innerHTML
								: rowB.cells[colNum].innerHTML - rowA.cells[colNum].innerHTML
						}
						break

					case 'date':
						compare = function (rowA, rowB) {
							var a = +stringToDate(rowA.cells[colNum].innerHTML)
							var b = +stringToDate(rowB.cells[colNum].innerHTML)
							return (way == 'up')
								? b > a ? 1 : -1
								: a > b ? 1 : -1
						}
						break

					case 'type':
						compare = function (rowA, rowB) {
							return way == 'up'
								? rowA.cells[colNum].querySelector('div').className > rowB.cells[colNum].querySelector('div').className ? 1 : -1
								: rowB.cells[colNum].querySelector('div').className > rowA.cells[colNum].querySelector('div').className ? 1 : -1
						}
						break

					case 'unique':
						compare = function (rowA, rowB) {
							return unique(rowA, rowB, way, colNum)
						}
						break

					default:
						compare = function (rowA, rowB) {
							return way == 'up'
								? rowA.cells[colNum].innerHTML > rowB.cells[colNum].innerHTML ? 1 : -1
								: rowB.cells[colNum].innerHTML > rowA.cells[colNum].innerHTML ? 1 : -1
						}
						break
				}

				for (var i = 0; i < headers.length; i++) headers[i].className = ''

				if (way == 'up') {
					this.setAttribute('way', 'down')
					this.className = 'sort__down'
				} else {
					this.setAttribute('way', 'up')
					this.className = 'sort__up'
				}

				rowsArray.sort(compare)

				table.removeChild(tbody)
				for (var i = 0; i < rowsArray.length; i++) tbody.appendChild(rowsArray[i])
				table.appendChild(tbody)
			}
		}
	}

	function stringToDate(s) {

		var d = [1, 1, 2000]
		var t = [1, 1, 1, 1]

		if (s.indexOf(' ') > -1) {
			var parts = s.split(' ')
			d = parts[0].split('.')
			t = parts[1].split(':')
		} else if (s.indexOf(':') > -1) {
			t = s.split(':')
		} else {
			d = s.split('.')
		}

		if (t[2].indexOf('.') > -1) {
			parts = t[2].split('.')
			t[2] = parts[0]
			t.push(parts[1])
		} else {
			t.push(0)
		}

		return new Date(d[2], d[1], d[0], t[0], t[1], t[2], t[3])
	}

	return Sort

})()