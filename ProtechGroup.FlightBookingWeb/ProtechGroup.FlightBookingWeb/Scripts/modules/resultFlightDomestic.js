
const listCardPrices = document.querySelectorAll(".list-card-price");
const planeItems = document.querySelectorAll(".plane-items");

planeItems.forEach((planeItem) => {
    planeItem.querySelector(".detail").addEventListener("click", () => {
        const listCardPrice = planeItem.querySelector(".list-card-price");
        listCardPrice.classList.toggle("hidden");
    });
});
listCardPrices.forEach((listCardPrice) => {
    const nextBtn = listCardPrice.querySelector(".next");
    const preBtn = listCardPrice.querySelector(".pre");
    nextBtn.addEventListener("click", () => {
        const cards = listCardPrice.querySelectorAll(".card-price");
        listCardPrice.appendChild(cards[0]); // đưa thẻ đầu ra cuối
    });
    preBtn.addEventListener("click", () => {
        const cards = listCardPrice.querySelectorAll(".card-price");
        const lastCard = cards[cards.length - 1];
        listCardPrice.insertBefore(lastCard, cards[0]);
    });
});

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("loading").style.display = "flex";
    document.getElementById("flight-list").style.display = "none";
});
let startTime = Date.now();
window.addEventListener("load", function () {
    let elapsed = Date.now() - startTime; 
    let remaining = 5000 - elapsed;      
    if (remaining < 0) remaining = 0;

    setTimeout(() => {
        document.getElementById("loading").style.display = "none";
        document.getElementById("flight-list").style.display = "block";
        document.getElementById("form-search").style.display = "block";
    }, remaining);
});

$(document).ready(function () {
    $('.action-item').click(function () {
        const sessionMod = $(this).attr('data-session');
        const dateMod = $(this).attr('data-strdate');
        const waytypeMod = $(this).attr('data-waytype');
        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
        var datapost = {
            __RequestVerificationToken: document.querySelector('input[name="__RequestVerificationToken"]').value,
            request: {
                SessionChange: sessionMod,
                DateChange: dateMod,
                WayTypeChange: waytypeMod
            }
        }
        $.ajax({
            url: '/Thaydoingaybay',
            type: 'POST',
            data: datapost,
            success: function (response) {
                if (response && response.dataResult) {
                    var elapsed = new Date().getTime() - startTime;
                    var remaining = 3000 - elapsed;
                    if (remaining < 0) remaining = 0;
                    setTimeout(function () {
                        postRedirect('/Ketquatimkiem', {
                            __RequestVerificationToken: token,
                            data: response.dataResult
                        });
                    }, remaining);
                } else {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Cảnh báo',
                        text: 'Xảy ra lỗi khi đặt vé!',
                        confirmButtonText: 'Đã hiểu'
                    });
                }
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Cảnh báo',
                    text: 'Xảy ra lỗi khi đặt vé!',
                    confirmButtonText: 'Đã hiểu'
                });
            }
        });
    });
    const btnOrder = document.getElementById('btn-order-flight');
    if (btnOrder) {
        btnOrder.addEventListener('click', function () {
            orderFlightConfirm($(this));
        });
    }
    document.querySelectorAll('.list-ticket-items-out .plane-items').forEach(itemTicket => {   
        const bntSelectFlight = itemTicket.querySelectorAll('.select-flight');
        bntSelectFlight.forEach(bntorderow => {
            bntorderow.addEventListener('click', function (e) {
                selectFlight(itemTicket,e.target);
            });
        });
    });
    document.querySelectorAll('.list-ticket-items-in .plane-items').forEach(itemTicket => {
        const bntSelectFlight = itemTicket.querySelectorAll('.select-flight');
        bntSelectFlight.forEach(bntorderow => {
            bntorderow.addEventListener('click', function (e) {
                selectFlight(itemTicket, e.target);
            });
        });
    });
    function validateSelectedFlights() {
        let roundTrip = document.getElementById("roundTrip").value.trim().toLowerCase() === "true";
        function checkSection(sectionId) {
            const el = document.getElementById(sectionId);
            if (!el) return false;
            const session = el.getAttribute("data-session");
            const codeAirline = el.getAttribute("data-codeairline");
            const keyBooking = el.getAttribute("data-keybooking");
            const way = el.getAttribute("data-way");
            if (!session || !codeAirline || !keyBooking || !way) {
                return false;
            }
            return true;
        }
        if (roundTrip === true) {
            const outValid = checkSection("select-out-bound");
            const inValid = checkSection("select-in-bound");
            return (outValid && inValid);
        }
        else {
            return checkSection("select-out-bound");
        }
    }
   
        function orderFlightConfirm(linkOrderElem) {
            if (validateSelectedFlights()) {
                let roundTrip = document.getElementById("roundTrip").value.trim().toLowerCase() === "true";
                var token = document.querySelector('input[name="__RequestVerificationToken"]').value;
                const rowOut = document.getElementById('select-out-bound');
                var data = [{
                    AirlineCode: rowOut.getAttribute('data-codeairline'),
                    SessionId: rowOut.getAttribute('data-session'),
                    BookingKey: rowOut.getAttribute('data-keybooking'),
                    WayType: 0
                }];
                if (roundTrip) {
                    const rowIn = document.getElementById('select-in-bound');
                    data.push({
                            AirlineCode: rowIn.getAttribute('data-codeairline'),
                            SessionId: rowIn.getAttribute('data-session'),
                            BookingKey: rowIn.getAttribute('data-keybooking'),
                            WayType: 1
                    });
                }
                var $link = linkOrderElem instanceof jQuery ? linkOrderElem : $(linkOrderElem);
                if ($link.data('processing')) return;
                $link.data('processing', true);
                var startTime = Date.now();
                var originalHtml = $link.html();
                $link.data('original-html', originalHtml);
                $link.addClass('disabled-link').css({
                    "pointer-events": "none",
                    "opacity": "0.6",
                    "display": "flex",
                    "flex-flow": "row",
                    "justify-content": "center",
                    "align-items": "center"
                });
                var datapost = {
                    __RequestVerificationToken: document.querySelector('input[name="__RequestVerificationToken"]').value,
                    request: data
                }
                $link.html('<img src="/Content/img/loading.gif" style="width:30px;height:30px;vertical-align:middle;margin-right:5px;margin-bottom:0;" /> Xử lý...');
                $.ajax({
                    url: '/Chonchuyenbay',
                    method: 'POST',
                    data: datapost,
                    success: function (response) {
                        if (response.status && response.dataResult) {
                            var elapsed = Date.now() - startTime;
                            var remaining = 3000 - elapsed;
                            if (remaining < 0) remaining = 0;
                            setTimeout(function () {
                                postRedirect('/Xacnhanthongtinkhachhang', {
                                    __RequestVerificationToken: token,
                                    data: response.dataResult
                                });
                            }, remaining);
                        } else {
                            Swal.fire({
                                icon: 'warning',
                                title: 'Cảnh báo',
                                text: 'Xảy ra lỗi chọn chuyến bay!',
                                confirmButtonText: 'Đã hiểu'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    return;
                                }
                            });
                            restoreLink();
                        }
                    },
                    error: function (xhr, status, error) {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Cảnh báo',
                            text: 'Xảy ra lỗi chọn chuyến bay!',
                            confirmButtonText: 'Đã hiểu'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                return;
                            }
                        });
                        restoreLink();
                    }
                });
            }
            else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Cảnh báo',
                    text: 'Bạn chưa chọn đủ chuyến bay cho hành trình đã chọn!',
                    confirmButtonText: 'Đã hiểu'
                }).then((result) => {
                    if (result.isConfirmed) {
                        return;
                    }
                });
            }
        }
        function selectFlight(itemTicket, linkOrderElem) {
            var $button = linkOrderElem instanceof jQuery ? linkOrderElem : $(linkOrderElem);
            const session = $button.attr('data-session');
            const airline = $button.attr('data-codeairline');
            const booking = $button.attr('data-keybooking') ;
            const waytype = $button.attr('data-way'); 
            const price = $button.attr('data-total-price');
            itemTicket.classList.add("selected-flight");
            if (waytype == 'OutBound') {
                let containerSelectFlight = document.getElementById("select-out-bound");
                document.querySelectorAll('.list-ticket-items-out .plane-items').forEach(item => {
                    if (!item.classList.contains("selected-flight"))
                        item.style.display = 'none';
                });
                containerSelectFlight.setAttribute("data-session", session);
                containerSelectFlight.setAttribute("data-codeairline", airline);
                containerSelectFlight.setAttribute("data-keybooking", booking);
                containerSelectFlight.setAttribute("data-way", waytype);
                containerSelectFlight.setAttribute("data-total-price", price);
                replaceSelectFlightHtml(session, airline, booking, 0);
            }
            else {
                let containerSelectFlight = document.getElementById("select-in-bound");
                document.querySelectorAll('.list-ticket-items-in .plane-items').forEach(item => {
                    if (!item.classList.contains("selected-flight"))
                        item.style.display = 'none';
                });
                containerSelectFlight.setAttribute("data-session", session);
                containerSelectFlight.setAttribute("data-codeairline", airline);
                containerSelectFlight.setAttribute("data-keybooking", booking);
                containerSelectFlight.setAttribute("data-way", waytype);
                containerSelectFlight.setAttribute("data-total-price", price);
                replaceSelectFlightHtml(session, airline, booking, 1);
            }
            sumPriceOrder();
        }
        function sumPriceOrder() {
            let totalPrice = 0;
            const elOut = document.getElementById('select-out-bound');
            const elIn = document.getElementById('select-in-bound');
            const priceOut = elOut.getAttribute('data-total-price');
            const priceIn = elIn.getAttribute('data-total-price');
            if(priceOut)
                totalPrice += parseInt(priceOut, 10);
            if(priceIn)
                totalPrice += parseInt(priceIn, 10);
            const priceFormat = formatNumber(totalPrice) + ' VNĐ';
            document.getElementById('display-price-order').textContent = priceFormat;
        }

        async function replaceSelectFlightHtml(session, airline, booking, waytype) {
            const url = "/Thongtinchuyenbay?airLineCode=" + airline + "&sessionId=" + session + "&bookingKey=" + booking + "&WayType=" + waytype;
            const responseFlight = await fetch(url);
            if (responseFlight != null) {
                const data = await responseFlight.json();
                let containerSelectFlight;
                if (waytype == 0)
                    containerSelectFlight = document.getElementById("select-out-bound");
                else
                    containerSelectFlight = document.getElementById("select-in-bound");
                containerSelectFlight.innerHTML = "";
                const disDepartureDate = formatDateVietnamese(parseDotNetDate(data.MainDepartureDate));
                const disDepartureDateFull = formatddMMyyyyVietnamese(parseDotNetDate(data.MainDepartureDate));
                const disArrivalDateFull = formatddMMyyyyVietnamese(parseDotNetDate(data.MainArrivalDate));
                var newHtml = `
                <div class="bg-[#CAF0F8] flex max-lg:flex-wrap items-start justify-between px-4 py-1.5 cursor-pointer select-none clicked">
                    <div>
                        <p class="font-bold text-base">Chuyến bay : ${data.MainDepartureCity} → ${data.MainArrivalCity}</p>
                        <p class="text-base">
                            <span class="max-md:block">${disDepartureDate}</span>
                            <span class="ml-3 max-md:ml-0 max-md:block ">${data.MainDepartureTime} - ${data.MainArrivalTime} (${data.Duration}, điểm dừng ${data.Stop})</span>
                        </p>
                    </div>
                    <div>
                        <p class="text-sm font-bold max-lg:mt-2">Hạng vé: ${data.PriceBreakDowns[0].ClassName}</p>
                    </div>
                </div>

                <div class="grid grid-cols-5 max-lg:grid-cols-2 max-md:block p-4">
                    <div class="flex flex-col max-lg:col-span-2 gap-4 max-lg:mb-5">
                        <div class="flex gap-3 items-start">
                            <div>
                                <img src="/Content/img/Airline/sm${data.MainAirlineCode}.gif" alt="" class="w-10" />
                            </div>
                            <div class="text-sm">
                                <p class="font-bold">${data.MainAirlineName}</p>
                                <p>${data.MainFlightNumber} - Plane: ${data.Plane}</p>
                            </div>
                        </div>
                    </div>

                    <div class="flex col-span-2 max-lg:col-span-1 gap-2 items-start justify-center max-md:justify-start max-md:mb-5">
                        <div>
                            <img src="/Content/img/flight-info/plane-start.svg" alt="" />
                        </div>
                        <div>
                            <div class="font-bold text-2xl">
                                <p>${data.MainDepartureAirportCode} - (${data.MainDepartureCity})</p>
                                <p>${data.MainDepartureAirportName}</p>
                            </div>
                            <p class="text-base">${disDepartureDateFull} - ${data.MainDepartureTime}</p>
                        </div>
                    </div>

                    <div class="flex col-span-2 max-lg:col-span-1 gap-2 items-start justify-center max-md:justify-start">
                        <div>
                            <img src="/Content/img/flight-info/plane-end.svg" alt="" />
                        </div>
                        <div>
                            <div class="font-bold text-2xl">
                                <p>${data.MainArrivalAirportCode} - (${data.MainArrivalCity})</p>
                                <p>${data.MainArrivalAirportName}</p>
                            </div>
                            <p class="text-base">${disArrivalDateFull} - ${data.MainArrivalTime}</p>
                        </div>
                    </div>
                </div>

                <div class="border-t border-[#EBA00A] border-dotted py-4 px-10 flex justify-between gap-10 max-md:flex-col max-md:gap-5 conditions">
                    <div class="flex gap-1.5">
                        <div><img src="/Content/img/flight-info/bag.svg" alt="" /></div>
                        <div>
                            <p class="text-base">Thông tin hành lý</p>
                            <ul class="my-1 text-sm">
                                <li>Mỗi hành khách có ${data.PriceBreakDowns[0].RecommendationNumber} và ${data.PriceBreakDowns[0].AllowanceBaggage}</li>
                            </ul>
                            <p class="text-[#878C93] text-sm">(Tổng hành lý đã bao gồm trong giá)</p>
                        </div>
                    </div>

                    <div class="flex gap-1.5">
                        <div><img src="/Content/img/flight-info/ticket.svg" alt="" /></div>
                        <div>
                            <p class="text-base">Điều kiện vé</p>
                            ${data.PriceBreakDowns[0].Condition}
                            <p class="text-[#878C93] text-sm">Cabin: ${data.PriceBreakDowns[0].CabinClass}.</p>
                        </div>
                    </div>
                </div>
            `;
                containerSelectFlight.insertAdjacentHTML("beforeend", newHtml);
            }
        }
    function orderFlight(itemTicket, linkOrderElem) {
        var $link = linkOrderElem instanceof jQuery ? linkOrderElem : $(linkOrderElem);
        if ($link.data('processing')) return;
        $link.data('processing', true);

        var $item = itemTicket instanceof jQuery ? itemTicket : $(itemTicket);
        const session = $item.attr('data-session') || $item.data('session');
        const airline = $item.attr('data-codeairline') || $item.data('codeairline');
        const booking = $item.attr('data-keybooking') || $item.data('keybooking');

        var data = [{
            AirlineCode: airline,
            SessionId: session,
            BookingKey: booking,
            WayType: 0
        }];
        var datapost = {
            __RequestVerificationToken: document.querySelector('input[name="__RequestVerificationToken"]').value,
            request: data
        }
        var startTime = Date.now();

        var originalHtml = $link.html();
        $link.data('original-html', originalHtml);
        $link.addClass('disabled-link').css({
            "pointer-events": "none",
            "opacity": "0.6",
            "display": "flex",
            "flex-flow": "row",
            "justify-content": "center",
            "align-items": "center"
        });

        $link.html('<img src="/Content/img/loading.gif" style="width:30px;height:30px;vertical-align:middle;margin-right:5px;margin-bottom:0;" /> Xử lý...');

        $.ajax({
            url: '/Chonchuyenbay',
            method: 'POST',
            data: datapost,
            success: function (response) {
                if (response && response.dataResult) {
                    var elapsed = Date.now() - startTime;
                    var remaining = 3000 - elapsed;
                    if (remaining < 0) remaining = 0;
                    setTimeout(function () {
                        postRedirect('/Xacnhanthongtinkhachhang', {
                            __RequestVerificationToken: document.querySelector('input[name="__RequestVerificationToken"]').value,
                            data: response.dataResult
                        });
                    }, remaining);
                } else {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Cảnh báo',
                        text: 'Xảy ra lỗi chọn chuyến bay!',
                        confirmButtonText: 'Đã hiểu'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            return;
                        }
                    });
                    restoreLink();
                }
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Cảnh báo',
                    text: 'Xảy ra lỗi chọn chuyến bay!',
                    confirmButtonText: 'Đã hiểu'
                }).then((result) => {
                    if (result.isConfirmed) {
                        return;
                    }
                });
                restoreLink();
            }
        });

        function restoreLink() {
            $link.html($link.data('original-html') || originalHtml);
            $link.removeClass('disabled-link').css({
                "pointer-events": "auto",
                "opacity": "1"
            });
            $link.data('processing', false);
        }
    }
    function updateTicketInfo(itemTicket, radio) {
        const selectedId = radio.id;
        itemTicket.querySelector('.typeRender').textContent = radio.value;
        itemTicket.querySelector('.price').textContent =
            new Intl.NumberFormat('vi-VN').format(radio.dataset.price) + 'đ';
        itemTicket.querySelectorAll('.ticket-info-type').forEach(info => {
            info.hidden = true;
        });
        const selectedInfo = document.querySelector(`.ticket-info-type[data-idtype="${selectedId}"]`);
        if (selectedInfo) {
            selectedInfo.hidden = false;
        }
        const keyprice = radio.getAttribute('data-keyprice');
        itemTicket.setAttribute('data-keybooking', keyprice); 
        
    }
});
class FlightFilter {
    constructor() {
        this.filters = {
            sort: [],
            airlines: [],
            departureTime: [],
            showPrice: []
        };
        this.init();
    }

    init() {
        this.setDefaultChecked();
        this.bindEvents();
        this.applyFilters();
    }
    setDefaultChecked() {
        const checkbox = document.querySelector('input[type="checkbox"][value="price"]');
        if (checkbox) {
            checkbox.checked = true;
            this.updateFilters();
            this.applyFilters();
        }
    }
    bindEvents() {
        document.querySelectorAll('.box-filter input[type="checkbox"]').forEach(checkbox => {
            checkbox.addEventListener('change', () => {
                this.updateFilters();
                this.applyFilters();
            });
        });
    }
    
    applyShowPrice() {
        let tickets = document.querySelectorAll('.list-ticket-items-out .plane-items .items-stretch');
        let ticketsIn = document.querySelectorAll('.list-ticket-items-in .plane-items .items-stretch');
        if (tickets) this.excuteShowPrice(tickets);
        if (ticketsIn) this.excuteShowPrice(ticketsIn);
    }
    excuteShowPrice(tickets) {
        tickets.forEach(item => {
            let priceTo = item.querySelector('.content-price .price');
            let priceEnd = item.querySelector('.content-price .price-end');
            let priceBaseAdtTo = parseInt(priceTo.getAttribute('data-price-base-adt'));
            let priceFullAdtTo = parseInt(priceTo.getAttribute('data-base-full-adt'));
            let priceFullPaxTo = parseInt(priceTo.getAttribute('data-price-full-pax'));
            let priceBaseAdtEnd = parseInt(priceEnd.getAttribute('data-price-base-adt'));
            let priceFullAdtEnd = parseInt(priceEnd.getAttribute('data-base-full-adt'));
            let priceFullPaxEnd = parseInt(priceEnd.getAttribute('data-price-full-pax'));
            let showPriceTo = '0';
            let showPriceEnd = '0';
            if (this.filters.showPrice.includes('priceBase')) {
                showPriceTo = formatPriceCustom(priceBaseAdtTo);
                showPriceEnd = formatPriceCustom(priceBaseAdtEnd);
                item.querySelector('p.price').innerHTML = `
                <span class="font-bold">${showPriceTo.head}</span><br />${showPriceTo.tail} VNĐ`;
                item.querySelector('p.price-end').innerHTML = `
                <span class="font-bold">${showPriceEnd.head}</span><br />${showPriceEnd.tail} VNĐ`;
                item.querySelectorAll('p.text-sm').forEach(p => { p.innerHTML = 'Giá 01 người lớn' });
            }
            if (this.filters.showPrice.includes('priceFullAdt') || this.filters.showPrice.length == 0) {
                showPriceTo = formatPriceCustom(priceFullAdtTo);
                showPriceEnd = formatPriceCustom(priceFullAdtEnd);
                item.querySelector('p.price').innerHTML = `
                <span class="font-bold">${showPriceTo.head}</span><br />${showPriceTo.tail} VNĐ`;
                item.querySelector('p.price-end').innerHTML = `
                <span class="font-bold">${showPriceEnd.head}</span><br />${showPriceEnd.tail} VNĐ`;
                item.querySelectorAll('p.text-sm').forEach(p => { p.innerHTML = 'Giá đủ 01 người lớn' });
            }
            if (this.filters.showPrice.includes('priceFullAllPax')) {
                showPriceTo = formatPriceCustom(priceFullPaxTo);
                showPriceEnd = formatPriceCustom(priceFullPaxEnd);
                item.querySelector('p.price').innerHTML = `
                <span class="font-bold">${showPriceTo.head}</span><br />${showPriceTo.tail} VNĐ`;
                item.querySelector('p.price-end').innerHTML = `
                <span class="font-bold">${showPriceEnd.head}</span><br />${showPriceEnd.tail} VNĐ`;
                item.querySelectorAll('p.text-sm').forEach(p => { p.innerHTML = 'Tổng tiền đơn hàng' });
            }
        });
    }
    updateFilters() {
        this.filters.sort = [];
        document.querySelectorAll('#box-filter-sx input[type="checkbox"]:checked').forEach(checkbox => {
            this.filters.sort.push(checkbox.value);
        });
        this.filters.airlines = [];
        document.querySelectorAll('#box-filter-lth input[type="checkbox"]:checked').forEach(checkbox => {
            this.filters.airlines.push(checkbox.value);
        });
        this.filters.departureTime = [];
        document.querySelectorAll('#box-filter-time input[type="checkbox"]:checked').forEach(checkbox => {
            const timeRange = checkbox.parentElement.querySelector('.time-filter').textContent.trim();
            this.filters.departureTime.push(timeRange);
        });
        this.filters.showPrice = [];
        document.querySelectorAll('#box-show-price input[type="checkbox"]:checked').forEach(checkbox => {
            this.filters.showPrice.push(checkbox.value);
        });
    }

    applyFilters() {
        const tickets = document.querySelectorAll('.plane-items');
        let visibleCount = 0;

        tickets.forEach(ticket => {
            let shouldShow = true;
            if (this.filters.airlines.length > 0) {
                const airlineCode = ticket.getAttribute('data-codeAirline');
                if (!this.filters.airlines.includes(airlineCode)) {
                    shouldShow = false;
                }
            }
            if (this.filters.departureTime.length > 0 && shouldShow) {
                const departureTime = ticket.querySelector('.departure .time').textContent.trim();
                const timeInMinutes = this.timeToMinutes(departureTime);

                if (!this.filters.departureTime.some(timeRange => {
                    return this.isTimeInRange(timeInMinutes, timeRange);
                })) {
                    shouldShow = false;
                }
            }
            if (shouldShow) {
                ticket.style.display = 'block';
                visibleCount++;
            } else {
                ticket.style.display = 'none';
            }
        });
        if (this.filters.sort.length > 0) {
            this.sortTickets();
        }
        if (this.filters.showPrice.length) {
            this.applyShowPrice();
        }
        this.showNoResultsMessage(visibleCount === 0);
    }

    timeToMinutes(timeStr) {
        const [hours, minutes] = timeStr.split(':').map(Number);
        return hours * 60 + minutes;
    }

    isTimeInRange(timeInMinutes, timeRange) {
        if (timeRange === '00:00 - 24:00') return true;

        const [start, end] = timeRange.split(' - ');
        const startMinutes = this.timeToMinutes(start);
        const endMinutes = this.timeToMinutes(end);

        if (endMinutes < startMinutes) {
            return timeInMinutes >= startMinutes || timeInMinutes <= endMinutes;
        } else {
            return timeInMinutes >= startMinutes && timeInMinutes <= endMinutes;
        }
    }

    sortTickets() {
        const tickets = Array.from(document.querySelectorAll('.item-out-bound:not([style*="display: none"])'));
        const container = document.querySelector('.list-ticket-items-out');

        const ticketsin = Array.from(document.querySelectorAll('.item-in-bound:not([style*="display: none"])'));
        const containerin = document.querySelector('.list-ticket-items-in');
        if (this.filters.sort.includes('price')) {
            tickets.sort((a, b) => {
                const priceA = this.extractPrice(a);
                const priceB = this.extractPrice(b);
                return priceA - priceB;
            });
        }
        if (this.filters.sort.includes('price')) {
            ticketsin.sort((a, b) => {
                const priceA = this.extractPrice(a);
                const priceB = this.extractPrice(b);
                return priceA - priceB;
            });
        }

        if (this.filters.sort.includes('time')) {
            tickets.sort((a, b) => {
                const timeA = this.extractTime(a);
                const timeB = this.extractTime(b);
                return timeA - timeB;
            });
        }
        if (this.filters.sort.includes('time')) {
            ticketsin.sort((a, b) => {
                const timeA = this.extractTime(a);
                const timeB = this.extractTime(b);
                return timeA - timeB;
            });
        }
        tickets.forEach(ticket => {
            container.appendChild(ticket);
        });
        ticketsin.forEach(ticket => {
            containerin.appendChild(ticket);
        });
    }

    extractPrice(ticket) {
        const priceText = ticket.getAttribute('data-price');
        return parseInt(priceText);
    }

    extractTime(ticket) {
        const timeText = ticket.querySelector('.departure .time').textContent;
        return this.timeToMinutes(timeText);
    }

    showNoResultsMessage(show) {
        let message = document.querySelector('.no-results-message');

        if (show) {
            if (!message) {
                message = document.createElement('div');
                message.className = 'no-results-message';
                message.innerHTML = `
                    <div style="text-align: center; padding: 40px; color: #666;font-size: 2.0rem;">
                      <h3>Không tìm thấy chuyến bay phù hợp</h3>
                      <p>Quý khách vui lòng thử lại với tìm kiếm hành trình khác</p>
                    </div>
                  `;
                document.querySelector('#loading').appendChild(message);
            }
        } else {
            if (message) {
                message.remove();
            }
        }
    }
    resetFilters() {
        document.querySelectorAll('.box-filter input[type="checkbox"]').forEach(checkbox => {
            checkbox.checked = false;
        });
        this.filters = { sort: [], airlines: [], departureTime: [] };
        this.applyFilters();
    }
    getActiveFiltersCount() {
        return this.filters.sort.length + this.filters.airlines.length + this.filters.departureTime.length;
    }
}
document.addEventListener('DOMContentLoaded', () => {
    window.flightFilter = new FlightFilter();

});

const style = document.createElement('style');
style.textContent = `
            .item-ticket {
              transition: all 0.3s ease;
            }

            .item-ticket[style*="display: none"] {
              opacity: 0;
              transform: scale(0.95);
            }

            .box-filter input[type="checkbox"]:checked + label {
              color: #0A7C79;
              font-weight: 500;
            }

            .filter .heading button:hover {
              background: #e0e0e0;
            }
          `;
document.head.appendChild(style);
document.addEventListener('DOMContentLoaded', () => {
    const backToTopBtn = document.getElementById('backToTop')
    window.addEventListener('scroll', () => {
        if (window.scrollY > 300) {
            backToTopBtn.style.display = 'block'
        } else {
            backToTopBtn.style.display = 'none'
        }
    })
    document.getElementById('backToTop').addEventListener('click', () => {
        scrollToTop(1000)
    })

    function scrollToTop(duration) {
        const start = window.scrollY
        const startTime = performance.now()

        function animate(currentTime) {
            const elapsed = currentTime - startTime
            const progress = Math.min(elapsed / duration, 1) 
            const ease = 1 - Math.pow(1 - progress, 3) 
            window.scrollTo(0, start * (1 - ease))
            if (elapsed < duration) {
                requestAnimationFrame(animate)
            }
        }
        requestAnimationFrame(animate)
    }
});
