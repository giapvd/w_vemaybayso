// ==== FORM SEARCH ====
// ELEMENT event
const formSearchInput = document.getElementById("form-search-input");
const departurePointInput = document.getElementById("departure-point-input");
const arrivalPointInput = document.getElementById("arrival-point-input");
const departureDateInput = document.getElementById("departure-date-input");
const returnDateInput = document.getElementById("return-date-input");
const personInput = document.getElementById("person-input");
const personInputChange = document.querySelectorAll(".person-input-change");
// ELEMENT value
const departurePointValue = document.getElementById("departure-point-value");
const arrivalPointValue = document.getElementById("arrival-point-value");
const departureDateValue = document.getElementById("departure-date-value");
const returnDateValue = document.getElementById("return-date-value");
const roundType = document.getElementById("round_type-value");

// Toggle location modal
const locationModal = document.getElementById("location-modal");
const dateModal = document.getElementById("date-modal");
const quantityModal = document.getElementById("quantity-modal");

// ==== EVENT LISTENER ====
// Mở modal location || date || quantity

let selectedInput = null;
let typePlane = "one_way"; // one_way || round_trip
const todayFix = new Date().toLocaleDateString("vi-VN");
departureDateValue.value = todayFix;
returnDateValue.value = todayFix;
departureDateInput.querySelector(".display").textContent = todayFix;
returnDateInput.querySelector(".display").textContent = todayFix;

personInput.addEventListener("click", (elem) => {
    selectedInput = "person";
    quantityModal.classList.toggle("hidden");
    locationModal.classList.add("hidden");
    dateModal.classList.add("hidden");
});
departurePointInput.addEventListener("click", (elem) => {
    selectedInput = "departure_point";
    locationModal.classList.toggle("hidden");
    quantityModal.classList.add("hidden");
    dateModal.classList.add("hidden");
});
arrivalPointInput.addEventListener("click", (elem) => {
    selectedInput = "arrival_point";
    locationModal.classList.toggle("hidden");
    quantityModal.classList.add("hidden");
    dateModal.classList.add("hidden");
});
departureDateInput.addEventListener("click", (elem) => {
    selectedInput = "departure_date";
    dateModal.classList.toggle("hidden");
    quantityModal.classList.add("hidden");
    locationModal.classList.add("hidden");
});
returnDateInput.addEventListener("click", (elem) => {
    selectedInput = "return_date";
    dateModal.classList.toggle("hidden");
    quantityModal.classList.add("hidden");
    locationModal.classList.add("hidden");
});
// Cập nhật số lượng hành khách
personInputChange.forEach((elem) => {
    const minusButton = elem.querySelector(".minus-button");
    const plusButton = elem.querySelector(".plus-button");
    const valueInput = elem.querySelector('input[type="text"]');
    let textRender = "";
    minusButton.addEventListener("click", () => {
        let currentValue = parseInt(valueInput.value);
        if (elem.dataset.type === "adult-passenger-value") {
            if (currentValue > 1) valueInput.value = currentValue - 1;
        } else {
            if (currentValue > 0) valueInput.value = currentValue - 1;
        }
        textRender =
            `${parseInt(
                document.querySelector('.person-input-change[data-type="adult-passenger-value"] input[type="text"]').value
            )}` +
            ` Người lớn, ${parseInt(
                document.querySelector('.person-input-change[data-type="child-passenger-value"] input[type="text"]').value
            )}` +
            ` Trẻ em, ${parseInt(
                document.querySelector('.person-input-change[data-type="infant-passenger-value"] input[type="text"]').value
            )}` +
            ` Em bé`;
        document.getElementById("render-person").textContent = textRender;
    });
    plusButton.addEventListener("click", () => {
        let currentValue = parseInt(valueInput.value);
        if (elem.dataset.type === "adult-passenger-value" || elem.dataset.type === "child-passenger-value") {
            const adultValue = parseInt(
                document.querySelector('.person-input-change[data-type="adult-passenger-value"] input[type="text"]').value
            );
            const childValue = parseInt(
                document.querySelector('.person-input-change[data-type="child-passenger-value"] input[type="text"]').value
            );
            if (adultValue + childValue < 7) valueInput.value = currentValue + 1;
        } else {
            if (currentValue < 5) valueInput.value = currentValue + 1;
        }
        textRender =
            `${parseInt(
                document.querySelector('.person-input-change[data-type="adult-passenger-value"] input[type="text"]').value
            )}` +
            ` Người lớn, ${parseInt(
                document.querySelector('.person-input-change[data-type="child-passenger-value"] input[type="text"]').value
            )}` +
            ` Trẻ em, ${parseInt(
                document.querySelector('.person-input-change[data-type="infant-passenger-value"] input[type="text"]').value
            )}` +
            ` Em bé`;
        document.getElementById("render-person").textContent = textRender;
    });
});

// Chọn địa điểm trong modal
function selecLocationModal(){
    locationModal.querySelectorAll(".location-option").forEach((elem) => {
        elem.addEventListener("click", (e) => {
            const name = e.target.closest(".location-option").querySelector(".name");
            const code = e.target.closest(".location-option").querySelector(".code");
            if (selectedInput === "departure_point") {
                departurePointInput.querySelector(".display").textContent = `${name.textContent} (${code.textContent})`;
                // SetValue location hidden input
                departurePointValue.value = code.textContent;
            } else if (selectedInput === "arrival_point") {
                arrivalPointInput.querySelector(".display").textContent = `${name.textContent} (${code.textContent})`;
                // SetValue location hidden input
                arrivalPointValue.value = code.textContent;
            }
            locationModal.classList.add("hidden");
        });
    });
}

// Change Một chiều || Khứ hồi form search
document.querySelectorAll('input[name="trip"]').forEach((elem) => {
    elem.addEventListener("change", function (event) {
        const value = event.target.id;
        typePlane = event.target.value;
        document.querySelectorAll(".days div").forEach((el) => el.classList.remove("start", "end", "range"));
        if (value === "one_way") {
            formSearchInput.classList.remove("grid-cols-5");
            formSearchInput.classList.add("grid-cols-4");
            departureDateInput.classList.add("max-xl:col-span-2");
            returnDateInput.classList.add("hidden");
            roundType.value=0;
        } else {
            formSearchInput.classList.add("grid-cols-5");
            formSearchInput.classList.remove("grid-cols-4");
            departureDateInput.classList.remove("max-xl:col-span-2");
            returnDateInput.classList.remove("hidden");
            roundType.value = 1;
        }
    });
});

// / CALENDER ÂM DƯƠNG LỊCH VIỆT NAM ====
function INT(d) {
    return Math.floor(d);
}
function jdFromDate(dd, mm, yy) {
    let a = INT((14 - mm) / 12);
    let y = yy + 4800 - a;
    let m = mm + 12 * a - 3;
    let jd = dd + INT((153 * m + 2) / 5) + 365 * y + INT(y / 4) - INT(y / 100) + INT(y / 400) - 32045;
    if (jd < 2299161) jd = dd + INT((153 * m + 2) / 5) + 365 * y + INT(y / 4) - 32083;
    return jd;
}
function jdToDate(jd) {
    let a, b, c;
    if (jd > 2299160) {
        a = jd + 32044;
        b = INT((4 * a + 3) / 146097);
        c = a - INT((b * 146097) / 4);
    } else {
        b = 0;
        c = jd + 32082;
    }
    let d = INT((4 * c + 3) / 1461);
    let e = c - INT((1461 * d) / 4);
    let m = INT((5 * e + 2) / 153);
    let day = e - INT((153 * m + 2) / 5) + 1;
    let month = m + 3 - 12 * INT(m / 10);
    let year = b * 100 + d - 4800 + INT(m / 10);
    return [day, month, year];
}
function getNewMoonDay(k, timeZone) {
    let T = k / 1236.85;
    let T2 = T * T,
        T3 = T2 * T;
    let dr = Math.PI / 180;
    let Jd1 = 2415020.75933 + 29.53058868 * k + 0.0001178 * T2 - 0.000000155 * T3;
    Jd1 += 0.00033 * Math.sin((166.56 + 132.87 * T - 0.009173 * T2) * dr);
    let M = 359.2242 + 29.10535608 * k - 0.0000333 * T2 - 0.00000347 * T3;
    let Mpr = 306.0253 + 385.81691806 * k + 0.0107306 * T2 + 0.00001236 * T3;
    let F = 21.2964 + 390.67050646 * k - 0.0016528 * T2 - 0.00000239 * T3;
    let C1 =
        (0.1734 - 0.000393 * T) * Math.sin(M * dr) +
        0.0021 * Math.sin(2 * M * dr) -
        0.4068 * Math.sin(Mpr * dr) +
        0.0161 * Math.sin(2 * Mpr * dr) +
        0.0104 * Math.sin(2 * F * dr);
    let deltat = T < -11 ? 0.001 + 0.000839 * T + 0.0002261 * T2 : -0.000278 + 0.000265 * T + 0.000262 * T2;
    return INT(Jd1 + C1 - deltat + 0.5 + timeZone / 24);
}
function getSunLongitude(jdn, timeZone) {
    let T = (jdn - 2451545.5 - timeZone / 24) / 36525;
    let T2 = T * T;
    let dr = Math.PI / 180;
    let M = 357.5291 + 35999.0503 * T - 0.0001559 * T2;
    let L0 = 280.46645 + 36000.76983 * T;
    let DL = (1.9146 - 0.004817 * T - 0.000014 * T2) * Math.sin(dr * M);
    let L = (L0 + DL) * dr;
    L = L - 2 * Math.PI * INT(L / (2 * Math.PI));
    return INT((L / Math.PI) * 6);
}
function getLunarMonth11(yy, timeZone) {
    let off = jdFromDate(31, 12, yy) - 2415021;
    let k = INT(off / 29.530588853);
    let nm = getNewMoonDay(k, timeZone);
    let sunLong = getSunLongitude(nm, timeZone);
    if (sunLong >= 9) nm = getNewMoonDay(k - 1, timeZone);
    return nm;
}
function getLunarDate(dd, mm, yy, timeZone = 7) {
    let dayNumber = jdFromDate(dd, mm, yy);
    let k = INT((dayNumber - 2415021.076998695) / 29.530588853);
    let monthStart = getNewMoonDay(k + 1, timeZone);
    if (monthStart > dayNumber) monthStart = getNewMoonDay(k, timeZone);
    let a11 = getLunarMonth11(yy, timeZone);
    let b11 = a11 >= monthStart ? getLunarMonth11(yy - 1, timeZone) : getLunarMonth11(yy + 1, timeZone);
    let diff = INT((monthStart - a11) / 29);
    let month = diff + 11;
    if (month > 12) month -= 12;
    let lunarDay = dayNumber - monthStart + 1;
    let lunarYear = month >= 11 && diff < 4 ? yy : yy + 1;
    return { day: lunarDay, month, year: lunarYear };
}

// ==== DỮ LIỆU ====
const today = new Date();
let currentYear = today.getFullYear();
let currentMonth = today.getMonth();
let startDate = null;
let endDate = null;

// Ngày lễ DƯƠNG lịch
const solarHolidays = [
    "01-01", // Tết Dương lịch
    "14-02", // Valentine
    "08-03", // Quốc tế Phụ nữ
    "30-04", // Giải phóng miền Nam
    "01-05", // Quốc tế Lao động
    "01-06", // Thiếu nhi
    "02-09", // Quốc khánh
    "20-11", // Ngày Nhà giáo Việt Nam
    "24-12", // Giáng sinh
];

// Ngày lễ ÂM lịch
const lunarHolidays = [
    "01-01", // Tết Nguyên Đán
    "02-01",
    "03-01", // Mùng 2,3 Tết
    "15-01", // Rằm tháng Giêng
    "10-03", // Giỗ Tổ Hùng Vương
    "15-07", // Vu Lan
    "15-08", // Trung Thu
];

// ==== HÀM RENDER ====
function renderCalendar(year, month) {
    const monthNames = [
        "Tháng Một",
        "Tháng Hai",
        "Tháng Ba",
        "Tháng Tư",
        "Tháng Năm",
        "Tháng Sáu",
        "Tháng Bảy",
        "Tháng Tám",
        "Tháng Chín",
        "Tháng Mười",
        "Tháng Mười Một",
        "Tháng Mười Hai",
    ];
    const weekdays = ["Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy", "Chủ Nhật"];
    const firstDay = new Date(year, month).getDay();
    const daysInMonth = new Date(year, month + 1, 0).getDate();
    let start = firstDay === 0 ? 6 : firstDay - 1;

    let html = `
        
        <div class="month">
          <div class="month-header">
            <span>${monthNames[month]}</span><span>${year}</span>
          </div>
          <div class="weekdays">${weekdays.map((d) => `<div>${d}</div>`).join("")}</div>
          <div class="days">`;
    for (let i = 0; i < start; i++) html += `<div class="inactive"></div>`;

    const startOfToday = new Date(today.getFullYear(), today.getMonth(), today.getDate());

    for (let day = 1; day <= daysInMonth; day++) {
        const date = new Date(year, month, day);
        const dateStr = [
            date.getFullYear(),
            String(date.getMonth() + 1).padStart(2, "0"),
            String(date.getDate()).padStart(2, "0"),
        ].join("-");
        const lunar = getLunarDate(day, month + 1, year);
        const solarKey = `${String(day).padStart(2, "0")}-${String(month + 1).padStart(2, "0")}`;
        const lunarKey = `${String(lunar.day).padStart(2, "0")}-${String(lunar.month).padStart(2, "0")}`;
        const isHoliday = solarHolidays.includes(solarKey) || lunarHolidays.includes(lunarKey);
        const isToday = date.toDateString() === today.toDateString();
        const isPast = date < startOfToday;

        html += `<div data-date="${dateStr}" class="${[
            isToday ? "today" : "",
            isHoliday ? "holiday" : "",
            isPast ? "disabled" : "",
        ].join(" ")}">
          ${day}<span class="lunar">${lunar.day}/${lunar.month}</span>
        </div>`;
    }
    html += `</div></div>`;
    return html;
}

function renderRangePicker() {
    const nextMonth = (currentMonth + 1) % 12;
    const nextYear = currentMonth === 11 ? currentYear + 1 : currentYear;
    document.getElementById("calendar").innerHTML = `
        <div class="calendar">
          ${renderCalendar(currentYear, currentMonth)}
          ${renderCalendar(nextYear, nextMonth)}
        </div>`;
    document.querySelectorAll(".days div[data-date]").forEach((day) => {
        if (!day.classList.contains("disabled")) day.addEventListener("click", () => handleDateClick(day));
    });
    updateRangeHighlight();
}

function handleDateClick(el) {
    // Hàm convert về Date chuẩn
    function toDate(dateStr) {
        const d = new Date(dateStr);
        return isNaN(d) ? null : d;
    }

    function formatDDMMYYYY(dateObj) {
        const d = new Date(dateObj);
        const dd = ("0" + d.getDate()).slice(-2);
        const mm = ("0" + (d.getMonth() + 1)).slice(-2);
        const yyyy = d.getFullYear();
        return `${dd}/${mm}/${yyyy}`;
    }
    const selectedDate = toDate(el.dataset.date);
    if (!selectedDate) return;
    if (typePlane === "one_way") {
        const dateFormatted = formatDDMMYYYY(selectedDate);
        departureDateInput.querySelector(".display").textContent = dateFormatted;
        departureDateValue.value = dateFormatted;
        dateModal.classList.add("hidden");
        return;
    }

    if (!startDate || (startDate && endDate)) {
        startDate = selectedDate;
        endDate = null;
    } else if (selectedDate < startDate) {
        endDate = startDate;
        startDate = selectedDate;
    } else {
        endDate = selectedDate;
    }

    updateRangeHighlight(); // gọi hàm đã fix trước đó
}

function updateRangeHighlight() {
    // Hàm so sánh ngày an toàn
    function isSameDay(a, b) {
        const da = new Date(a);
        const db = new Date(b);
        if (isNaN(da) || isNaN(db)) return false;
        return da.getFullYear() === db.getFullYear() &&
            da.getMonth() === db.getMonth() &&
            da.getDate() === db.getDate();
    }

    // Hàm ép đúng về Date
    function toDate(dateStr) {
        const d = new Date(dateStr);
        return isNaN(d) ? null : d;
    }

    // Xóa class cũ
    document.querySelectorAll(".days div").forEach((el) =>
        el.classList.remove("start", "end", "range")
    );

    if (!startDate) return;

    // Duyệt tất cả ngày
    document.querySelectorAll(".days div[data-date]").forEach((el) => {
        const d = toDate(el.dataset.date);
        if (!d) return;

        if (isSameDay(d, startDate)) el.classList.add("start");
        if (endDate && isSameDay(d, endDate)) el.classList.add("end");

        if (endDate && d > startDate && d < endDate) {
            el.classList.add("range");
        }
    });

    // Cập nhật text hiển thị ngày
    function formatDDMMYYYY(dateObj) {
        const d = new Date(dateObj);
        const dd = ("0" + d.getDate()).slice(-2);
        const mm = ("0" + (d.getMonth() + 1)).slice(-2);
        const yyyy = d.getFullYear();
        return `${dd}/${mm}/${yyyy}`;
    }

    if (startDate && endDate) {
        departureDateInput.querySelector(".display").textContent = formatDDMMYYYY(startDate);
        departureDateValue.value = formatDDMMYYYY(startDate);

        returnDateInput.querySelector(".display").textContent = formatDDMMYYYY(endDate);
        returnDateValue.value = formatDDMMYYYY(endDate);

        dateModal.classList.add("hidden");
    } else if (startDate) {
        departureDateInput.querySelector(".display").textContent = formatDDMMYYYY(startDate);
        departureDateValue.value = formatDDMMYYYY(startDate);
    }
}

document.getElementById("prev").onclick = () => {
    currentMonth--;
    if (currentMonth < 0) {
        currentMonth = 11;
        currentYear--;
    }
    renderRangePicker();
};
document.getElementById("next").onclick = () => {
    currentMonth++;
    if (currentMonth > 11) {
        currentMonth = 0;
        currentYear++;
    }
    renderRangePicker();
};

renderRangePicker();

const locationOptions = document.querySelectorAll(".location-option");
const typeCityIp = document.querySelectorAll(".typeCity");
// fillter Modal city
function filterModalCity(value) {
    locationOptions.forEach((locationOption) => {
        if (locationOption.dataset.citytype === value) {
            locationOption.hidden = false;
        } else {
            locationOption.hidden = true;
        }
    });
}
filterModalCity(typeCityIp[0].value);

const airportsVietNam = [
    { code: "HAN", cityname: "Hà Nội" },
    { code: "SGN", cityname: "Hồ Chí Minh" },
    { code: "DAD", cityname: "Đà Nẵng" },
    { code: "DIN", cityname: "Điện Biên Phủ" },
    { code: "HPH", cityname: "Hải Phòng" },
    { code: "THD", cityname: "Thanh Hóa" },
    { code: "VII", cityname: "Vinh" },
    { code: "VDH", cityname: "Quảng Bình" },
    { code: "VCA", cityname: "Cần Thơ" },
    { code: "VKG", cityname: "Kiên Giang" },
    { code: "VCL", cityname: "Quảng Nam" },
    { code: "HUI", cityname: "Huế" },
    { code: "PXU", cityname: "PleiKu" },
    { code: "TBB", cityname: "Phú Yên" },
    { code: "BMV", cityname: "Ban Mê Thuột" },
    { code: "CXR", cityname: "Nha Trang" },
    { code: "UIH", cityname: "Qui Nhơn" },
    { code: "DLI", cityname: "Đà Lạt" },
    { code: "CAH", cityname: "Cà Mau" },
    { code: "PQC", cityname: "Phú Quốc" },
    { code: "VCS", cityname: "Côn Đảo" },
    { code: "VDO", cityname: "Vân Đồn" }
];
const airportsAsia = [
    { code: "SIN", cityname: "Singapore" },
    { code: "HLP", cityname: "Jakarta" },
    { code: "BKK", cityname: "Băng Cốc" },
    { code: "DPS", cityname: "Bali Denpasar" },
    { code: "KUL", cityname: "Kuala Lumpur" },
    { code: "MNL", cityname: "Manila" },
    { code: "RGN", cityname: "Yangon" },
    { code: "PNH", cityname: "Phnôm Pênh" },
    { code: "REP", cityname: "Siem Reap" },
    { code: "VTE", cityname: "Viên Chăn" },
    { code: "LPQ", cityname: "Luông pra băng" },
    { code: "PEK", cityname: "Bắc Kinh" },
    { code: "PVG", cityname: "Thượng Hải" },
    { code: "SHA", cityname: "Thượng Hải" },
    { code: "CAN", cityname: "Quảng Châu" },
    { code: "TFU", cityname: "Thành Đô" },
    { code: "TPE", cityname: "Đài Bắc" },
    { code: "RMQ", cityname: "Đài Trung" },
    { code: "ICN", cityname: "Seoul" },
    { code: "GMP", cityname: "Seoul" },
    { code: "PUS", cityname: "Pusan" },
    { code: "KHH", cityname: "Cao Hùng" },
    { code: "TNN", cityname: "Đài Nam" },
    { code: "HKG", cityname: "Hồng Kông" },
    { code: "NRT", cityname: "Tokyo" },
    { code: "HND", cityname: "Tokyo" },
    { code: "NGO", cityname: "Nagoya" },
    { code: "FUK", cityname: "Fukuoka" },
    { code: "KIX", cityname: "Osaka" },
    { code: "BOM", cityname: "Mumbai" },
    { code: "DEL", cityname: "Đê-li" },
    { code: "KTM", cityname: "Kathmandu" },
    { code: "DAC", cityname: "Dhaka" },
    { code: "IST", cityname: "Istanbul" },
    { code: "DXB", cityname: "Dubai" },
    { code: "CMB", cityname: "Colombo" },
    { code: "CCU", cityname: "Kolkata" },
    
];
const airportsEurope = [
    { code: "CDG", cityname: "Paris" },
    { code: "LHR", cityname: "Luân Đôn" },
    { code: "MAN", cityname: "Manchester" },
    { code: "TXL", cityname: "Berlin" },
    { code: "FRA", cityname: "Frankfurt" },
    { code: "FCO", cityname: "Rome" },
    { code: "VIE", cityname: "Viên" },
    { code: "AMS", cityname: "Amsterdam" },
    { code: "MAD", cityname: "Madrid" },
    { code: "DME", cityname: "Moscow" },
    { code: "GVA", cityname: "Geneva" },
    { code: "PRG", cityname: "Praha" },
    { code: "CPH", cityname: "Copenhagen "},
];
const airportsAmericaCanada = [
    { code: "JFK", cityname: "New York" },
    { code: "DCA", cityname: "Washington" },
    { code: "LAX", cityname: "Los Angeles" },
    { code: "SFO", cityname: "San Francisco" },
    { code: "ALT", cityname: "Atlanta" },
    { code: "BOS", cityname: "Boston" },
    { code: "SEA", cityname: "Seattle" },
    { code: "STL", cityname: "St Louis" },
    { code: "YVR", cityname: "Vancouver" },
    { code: "RFD", cityname: "Chicago" },
    { code: "DFW", cityname: "Dallas" },
    { code: "DEN", cityname: "Denver" },
    { code: "HNL", cityname: "Honolulu" },
    { code: "MIA", cityname: "Miami" },
    { code: "MSP", cityname: "Minneapolis" },
    { code: "PHL", cityname: "Philadelphia" },
    { code: "PDX", cityname: "Portland (Oregon)" },
    { code: "YYZ", cityname: "Toronto" },
    { code: "YOW", cityname: "Ottawa" },
    { code: "YMX", cityname: "Montreal" }
];
const airportsAustraAfrica = [
    { code: "MEL", cityname: "Melbourne" },
    { code: "SYD", cityname: "Sydney" },
    { code: "ADL", cityname: "Adelaide" },
    { code: "BNE", cityname: "Brisbane" },
    { code: "MEL", cityname: "Brisbane (Auckland)" },
    { code: "WLG", cityname: "Wellington" },
    { code: "NBO", cityname: "Nairobi" },
    { code: "MPM", cityname: "Maputo" },
    { code: "LAD", cityname: "Luanda" },
    { code: "JNB", cityname: "Johannesburg" },
    { code: "CPT", cityname: "Cape Town" },
    { code: "DAR", cityname: "Dar Es Salaam" }
];
let airportsSearch = [];
typeCityIp.forEach((elem) => {
    elem.onclick = () => {
        // Xóa hết background checked
        typeCityIp.forEach((e) => {
            e.closest("label").classList.remove("bg-primary", "text-white");
        });
        // add background checked cho item đang checked
        addAirport(elem.value);
        elem.closest("label").classList.add("bg-primary", "text-white");
    };
});
function addAirport(value) {
    const titleAirport = document.getElementById("title-airport");
    const containerAirport = document.getElementById("contaner-airport");
    containerAirport.innerHTML = "";
    if (value === "vietnam") {
        titleAirport.textContent = "Nội địa - Sân bay Việt Nam";
        airportsVietNam.forEach((ap) => {
            const div = document.createElement("div");
            div.className = "flex gap-1 items-end border-b cursor-pointer border-[#CAF0F8] pb-3 location-option";
            div.setAttribute("data-cityType", "vietnam");
            div.innerHTML = `
                <p class="font-bold text-[32px] uppercase code">${ap.code}</p>
                <p class="text-xl name">${ap.cityname}</p>
            `;
            containerAirport.appendChild(div);
        });
    }
    if (value === "chaua") {
        titleAirport.textContent = "Sân bay Châu Á";
        airportsAsia.forEach((ap) => {
            const div = document.createElement("div");
            div.className = "flex gap-1 items-end border-b cursor-pointer border-[#CAF0F8] pb-3 location-option";
            div.setAttribute("data-cityType", "chaua");
            div.innerHTML = `
                <p class="font-bold text-[32px] uppercase code">${ap.code}</p>
                <p class="text-xl name">${ap.cityname}</p>
            `;
            containerAirport.appendChild(div);
        });
    }
    if (value === "chauau") {
        titleAirport.textContent = "Sân bay Châu Âu";
        airportsEurope.forEach((ap) => {
            const div = document.createElement("div");
            div.className = "flex gap-1 items-end border-b cursor-pointer border-[#CAF0F8] pb-3 location-option";
            div.setAttribute("data-cityType", "chauau");
            div.innerHTML = `
                <p class="font-bold text-[32px] uppercase code">${ap.code}</p>
                <p class="text-xl name">${ap.cityname}</p>
            `;
            containerAirport.appendChild(div);
        });
    }
    if (value === "hoaky-canada") {
        titleAirport.textContent = "Sân bay Hoa Kỳ - Canada";
        airportsAmericaCanada.forEach((ap) => {
            const div = document.createElement("div");
            div.className = "flex gap-1 items-end border-b cursor-pointer border-[#CAF0F8] pb-3 location-option";
            div.setAttribute("data-cityType", "hoaky-canada");
            div.innerHTML = `
                <p class="font-bold text-[32px] uppercase code">${ap.code}</p>
                <p class="text-xl name">${ap.cityname}</p>
            `;
            containerAirport.appendChild(div);
        });
    }
    if (value === "chauuc-phi") {
        titleAirport.textContent = "Sân bay Châu Úc - Phi";
        airportsAustraAfrica.forEach((ap) => {
            const div = document.createElement("div");
            div.className = "flex gap-1 items-end border-b cursor-pointer border-[#CAF0F8] pb-3 location-option";
            div.setAttribute("data-cityType", "chauuc-phi");
            div.innerHTML = `
                <p class="font-bold text-[32px] uppercase code">${ap.code}</p>
                <p class="text-xl name">${ap.cityname}</p>
            `;
            containerAirport.appendChild(div);
        });
    }
    if (value === "search-airport") {
        if (airportsSearch.length > 0)
            titleAirport.textContent = "Danh sách tìm kiếm";
        else
            titleAirport.textContent = "Không tìm thấy sân bay";
        airportsSearch.forEach((ap) => {
            const div = document.createElement("div");
            div.className = "flex gap-1 items-end border-b cursor-pointer border-[#CAF0F8] pb-3 location-option";
            div.setAttribute("data-cityType", "search-airport");
            div.innerHTML = `
                <p class="font-bold text-[32px] uppercase code">${ap.code}</p>
                <p class="text-xl name">${ap.cityname}</p>
            `;
            containerAirport.appendChild(div);
        });
    }
    selecLocationModal();
}
addAirport("vietnam");
///////// Custom select /////////
document.querySelectorAll(".select-customer-wrapper").forEach((elem) => {
    elem.addEventListener("click", () => {
        elem.querySelector(".select-customer-dropdown").classList.toggle("hidden");
    });
    elem.querySelectorAll(".option").forEach((option) => {
        option.addEventListener("click", (e) => {
            const selectedText = e.target.closest(".option").textContent.trim();
            elem.querySelector(".text-render").textContent = selectedText;
            elem.querySelector('input[type="hidden"]').value = e.target.closest(".option").dataset.value;
        });
    });
});
