
function formatNumber(num) {
    if (isNaN(num)) return '';
    return Number(num).toLocaleString('en-US');
}
function postRedirect(url, data) {
    const form = document.createElement('form');
    form.method = 'POST';
    form.action = url;

    for (const key in data) {
        if (data.hasOwnProperty(key)) {
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = key;
            input.value = data[key];
            form.appendChild(input);
        }
    }
    document.body.appendChild(form);
    form.submit();
}
function formatPriceCustom(number) {
    let str = number.toString();         // "2289000"
    let reversed = str.split("").reverse();

    let arr = [];
    for (let i = 0; i < reversed.length; i++) {
        if (i > 0 && i % 3 === 0) arr.push(","); // thêm dấu phẩy sau mỗi 3 số
        arr.push(reversed[i]);
    }

    let formatted = arr.reverse().join(""); // "2,289,000"

    // Tách phần cuối 3 số
    let last3 = formatted.slice(-3);      // "000"
    let head = formatted.slice(0, -3);    // "2,289,"

    if (!head.endsWith(",")) head += ","; // đảm bảo ký tự cuối là dấu phẩy

    return {
        head: head,
        tail: last3
    };
}
function parseDotNetDate(dotNetDate) {
    const timestamp = parseInt(dotNetDate.replace("/Date(", "").replace(")/", ""));
    return new Date(timestamp);
}
function formatDateVietnamese(date) {
    const days = ["Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7"];

    const dayName = days[date.getDay()];
    const day = date.getDate();
    const month = date.getMonth() + 1;

    return `${dayName}, ngày ${day} tháng ${month}`;
}
function formatddMMyyyyVietnamese(date) {
    const days = ["Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7"];

    const dayName = days[date.getDay()];
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getYear();

    return `${dayName}, ${day}/${month}/${year}`;
}
function removeVietnameseTones(str) {
    str = str.normalize("NFD").replace(/[\u0300-\u036f]/g, ""); // Bỏ dấu tổ hợp
    str = str.replace(/đ/g, "d").replace(/Đ/g, "D");
    return str;
}
