document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("loading").style.display = "flex";
    document.getElementById("flight-list").style.display = "none";
    document.getElementById("form-search").style.display = "none";
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
    $(".toggle-info").click(function () {
        const $btn = $(this);
        const target = $btn.data("target");
        const $info = $(target);
        $info.slideToggle(300, function () {
            $btn.html(
                $info.is(":visible") ?
                    'Rút gọn <svg style="transform: rotate(90deg);" width="8" height="13" viewBox="0 0 8 13" fill="none" xmlns="http://www.w3.org/2000/svg"> <path d="M1.5 1.5L6.5 6.5L1.5 11.5" stroke="#FF3C13" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" /></svg>' :
                    'Chi tiết hạng vé  <svg width="8" height="13" viewBox="0 0 8 13" fill="none" xmlns="http://www.w3.org/2000/svg"> <path d="M1.5 1.5L6.5 6.5L1.5 11.5" stroke="#FF3C13" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" /></svg>'
            );
        });
    });
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
    
    document.querySelectorAll('.item-ticket-out').forEach(itemTicket => {
        const radios = itemTicket.querySelectorAll('.inputChangeType');
        const bntorderow = itemTicket.querySelectorAll('.btn-order-oneway');
        const bntselectrow = itemTicket.querySelectorAll('.btn-select-round-trip');
        if (radios.length === 0) return;

        const firstRadio = radios[0];
        firstRadio.checked = true;
        updateTicketInfo(itemTicket, firstRadio);
        radios.forEach(radio => {
            radio.addEventListener('change', function (e) {
                updateTicketInfo(itemTicket, e.target);
            });
        });
        bntorderow.forEach(bntorderow => {
            bntorderow.addEventListener('click', function (e) {
                orderFlightOneWay(itemTicket, e.target);
            });
        });
        bntselectrow.forEach(bntorderow => {
            bntorderow.addEventListener('click', function (e) {
                selectFlightroundtrip(itemTicket, 0);
            });
        });
    });
    document.querySelectorAll('.item-ticket-in').forEach(itemTicket => {
        const radios = itemTicket.querySelectorAll('.inputChangeType');
        const bntselectrow = itemTicket.querySelectorAll('.btn-select-round-trip');
        if (radios.length === 0) return;

        const firstRadio = radios[0];
        firstRadio.checked = true;
        updateTicketInfo(itemTicket, firstRadio);

        radios.forEach(radio => {
            radio.addEventListener('change', function (e) {
                updateTicketInfo(itemTicket, e.target);
            });
        });
        bntselectrow.forEach(bntorderow => {
            bntorderow.addEventListener('click', function (e) {
                selectFlightroundtrip(itemTicket, 1);
            });
        });
    });
    
    function removeSelectFlight(itemRow, wayType) {
        itemRow.innerHTML = '';
        itemRow.removeAttribute('data-codeairline');
        itemRow.removeAttribute('data-session');
        itemRow.removeAttribute('data-keybooking');
        if (wayType == 0) {
            document.querySelectorAll('.item-out-bound').forEach(item => {
                item.style.display = 'block';
            });
        }
        else {
            document.querySelectorAll('.item-in-bound').forEach(item => {
                item.style.display = 'block';
            });
        }
        if (checkDepositedEmpty()) {
            document.querySelector('.is-deposited').style.display = 'none';
        }

    }
    function checkDepositedEmpty() {
        const deposited = document.querySelector('.is-deposited');
        if (!deposited) return false;

        const rowOut = deposited.querySelector('.row-out-bound');
        const rowIn = deposited.querySelector('.row-in-bound');
        const isRowOutEmpty = !rowOut || rowOut.innerHTML.trim() === '';
        const isRowInEmpty = !rowIn || rowIn.innerHTML.trim() === '';

        return isRowOutEmpty && isRowInEmpty;
    }
    function selectFlightroundtrip(itemTicket, waytype) {
        
        let strInfoPrice = '';
        const idRadioChecked = itemTicket.querySelector('input.inputChangeType:checked').id
        var $item = itemTicket instanceof jQuery ? itemTicket : $(itemTicket);
        const session = $item.attr('data-session') || $item.data('session');
        const airline = $item.attr('data-codeairline') || $item.data('codeairline');
        const bookingkey = $item.attr('data-keybooking') || $item.data('keybooking');
      
        if (waytype == 0) {
            strInfoPrice = '<p class="color-dark">Tổng tiền chiều đi:</p>';
            document.querySelectorAll('.item-out-bound').forEach(item => {
                item.style.display = 'none';
            });
        }
        else {
            document.querySelectorAll('.item-in-bound').forEach(item => {
                item.style.display = 'none';
            });
            strInfoPrice = '<p class="color-dark">Tổng tiền chiều về:</p>';
        }
            
        let htmlcontainer = '<div class="col-3 col-lg-4">' +
            '<div class="airline-info">' +
            itemTicket.querySelector('img.img-logo-airline').outerHTML +
            '<div class="airline-name">' +
            itemTicket.querySelector('.airline-name span.name').outerHTML +
            itemTicket.querySelector('.airline-name span.code').outerHTML +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="col-4 col-lg-8">' +
            '<div class="flight-details">' +
            '<div class="time-info">' +
            '<div class="departure">' +
            itemTicket.querySelector('.flight-details .departure div.time').outerHTML +
            itemTicket.querySelector('.flight-details .departure div.location').outerHTML +
            itemTicket.querySelector('.flight-details .departure div.date').outerHTML +
            '</div>' +
            '<div class="arrow-item">' +
            itemTicket.querySelector('.flight-details .arrow-item p.duration').outerHTML +
            '<div class="arrow"><span class="d-none-lg">---</span> ✈ <span class="d-none-lg">---</span></div>' +
            itemTicket.querySelector('.flight-details .arrow-item p.numstop').outerHTML +
            '</div>' +
            '<div class="arrival">' +
            itemTicket.querySelector('.flight-details .arrival div.time').outerHTML +
            itemTicket.querySelector('.flight-details .arrival div.location').outerHTML +
            itemTicket.querySelector('.flight-details .arrival div.date').outerHTML +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="line col-12 d-none d-block-lg"></div>' +
            '<div class="col-3 col-lg-4">' +
            '<div class="flight-meta">' +
            strInfoPrice +
            itemTicket.querySelector(`.type${idRadioChecked} span.total-price`).outerHTML +
            '</div>' +
            '</div>' +
            '<div class="col-2 col-lg-8">' +
            '<div class="box-checked d-none-lg">' +
            '<p class="item-checked">Thay đổi</p>' +
            '</div>';
        if (waytype == 1) {
            htmlcontainer += '<div><a class="btn btn-order-flight-round-trip" style="width: 140px;">Đặt vé</a></div>';
        }
        htmlcontainer += '</div></div></div>';
        appendItemToDepositedRow('is-deposited', htmlcontainer, waytype, session, airline, bookingkey)
    }
    
    function appendItemToDepositedRow(nameCalassContainer, htmlContainer, waytype, session, airline, bookingkey) {
        const container = document.querySelector('.' + nameCalassContainer);
        container.style.display = 'block';
        if (!container) {
            console.error('Không tìm thấy .' + nameCalassContainer + ' trong DOM!');
            return;
        }
        let newRow;
        if (waytype == 1) {
            newRow = container.querySelector('.row-in-bound');
        }
        else {
            newRow = container.querySelector('.row-out-bound');
        }
        newRow.setAttribute('data-codeairline', airline);
        newRow.setAttribute('data-session', session);
        newRow.setAttribute('data-keybooking', bookingkey);  
        if (htmlContainer instanceof HTMLElement) {
            newRow.innerHTML = htmlContainer.outerHTML;
        }
        else if (typeof htmlContainer === 'string') {
            newRow.innerHTML = htmlContainer.trim();
        }
        else {
            console.error('itemTicket không hợp lệ:', htmlContainer);
            return;
        }
        const rowOut = container.querySelector('.row-out-bound');
        const rowIn = container.querySelector('.row-in-bound');
        const pOut = rowOut.querySelector('.box-checked');
        const pIn = rowIn.querySelector('.box-checked');
        const btnOrder = rowIn.querySelector('.btn-order-flight-round-trip');
        if (pOut && waytype == 0) {
            pOut.addEventListener('click', function (e) {
                removeSelectFlight(rowOut, waytype);
            });
        }
        if (pIn && waytype==1) {
            pIn.addEventListener('click', function (e) {
                removeSelectFlight(rowIn, waytype);
            });
        }
        if (btnOrder) {
            btnOrder.addEventListener('click', function () {
                orderFlightRoundTrip($(this));
            });
        }
    }
    function orderFlightRoundTrip(linkOrderElem) {
        const deposited = document.querySelector('.is-deposited');
        var token = document.querySelector('input[name="__RequestVerificationToken"]').value;
        if (!deposited) return false;

        const rowOut = deposited.querySelector('.row-out-bound');
        const rowIn = deposited.querySelector('.row-in-bound');
        const isRowOutEmpty = !rowOut || rowOut.innerHTML.trim() === '';
        const isRowInEmpty = !rowIn || rowIn.innerHTML.trim() === '';
        
        if (!isRowOutEmpty && !isRowInEmpty)
        {
            var data = [{
                AirlineCode: rowOut.getAttribute('data-codeairline'),
                SessionId: rowOut.getAttribute('data-session'),
                BookingKey: rowOut.getAttribute('data-keybooking'),
                        WayType: 0
                    },
                    {
                        AirlineCode: rowIn.getAttribute('data-codeairline'),
                        SessionId: rowIn.getAttribute('data-session'),
                        BookingKey: rowIn.getAttribute('data-keybooking'),
                        WayType: 1
                }];
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
                text: 'Bạn chưa chọn đủ chuyến bay!',
                confirmButtonText: 'Đã hiểu'
            }).then((result) => {
                if (result.isConfirmed) {
                    return;
                }
            });
        }
    }
    function orderFlightOneWay(itemTicket, linkOrderElem) {
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
            departureTime: []
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
            const timeRange = checkbox.parentElement.querySelector('.time').textContent.trim();
            this.filters.departureTime.push(timeRange);
        });
    }

    applyFilters() {
        const tickets = document.querySelectorAll('.item-ticket');
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
        const tickets = Array.from(document.querySelectorAll('.item-ticket-out:not([style*="display: none"])'));
        const container = document.querySelector('.list-ticket-items-out');

        const ticketsin = Array.from(document.querySelectorAll('.item-ticket-in:not([style*="display: none"])'));
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
        const priceText = ticket.querySelector('.price').textContent;
        return parseInt(priceText.replace(/[^\d]/g, ''));
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
                document.querySelector('.list-ticket-items').appendChild(message);
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
