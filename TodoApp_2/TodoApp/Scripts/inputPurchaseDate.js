﻿window.onload = () => {
    const date = new Date()
    let year = date.getFullYear()
    let month = date.getMonth() + 1
    let day = date.getDate()
 
    let toTwoDigits = (num, digit) => {
        num += ''
        if (num.length < digit) {
            num = '0' + num
        }
        return num
    }

    const yyyy = toTwoDigits(year, 4)
    const mm = toTwoDigits(month, 2)
    const dd = toTwoDigits(day, 2)
    let ymd = yyyy + "/" + mm + "/" + dd;
 
    document.getElementById("PurchaseDate").value = ymd;
}

var $input = $('.datepicker').pickadate({
    formatSubmit: 'yyyy/mm/dd',
    // min: [2015, 7, 14],
    container: '#container',
    // editable: true,
    closeOnSelect: false,
    closeOnClear: false,
})