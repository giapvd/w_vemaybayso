function isValidDate(dateString) {
    var regex = /^([0-2][0-9]|3[0-1])\/(0[1-9]|1[0-2])\/\d{4}$/;

    if (!regex.test(dateString)) {
        return false;
    }

    var parts = dateString.split("/");
    var day = parseInt(parts[0], 10);
    var month = parseInt(parts[1], 10) - 1; 
    var year = parseInt(parts[2], 10);

    var date = new Date(year, month, day);

    return (date.getDate() === day &&
        date.getMonth() === month &&
        date.getFullYear() === year);
}
function isValidDateRange(startDateStr, endDateStr) {
    function parseDate(str) {
        let parts = str.split("/");
        if (parts.length !== 3) return null;

        let day = parseInt(parts[0], 10);
        let month = parseInt(parts[1], 10) - 1; 
        let year = parseInt(parts[2], 10);

        let date = new Date(year, month, day);

        if (date.getFullYear() !== year || date.getMonth() !== month || date.getDate() !== day) {
            return null;
        }
        return date;
    }

    let startDate = parseDate(startDateStr);
    let endDate = parseDate(endDateStr);

    if (!startDate || !endDate) {
        return false; 
    }

    return startDate < endDate; 
}