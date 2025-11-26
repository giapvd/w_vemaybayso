
$(document).ready(function () {
    var $checkBoxContactl = $("#is-chose-info-contactl");
    var $firstInput = $("input.passenger-name-adt").first();
    var $checkboxInvoice = $('#is-invoice');
    var $emailContactl = $(".email-contactl");
    if ($firstInput.length === 0) return;
    $firstInput.on("input", function (e) {
        if ($checkBoxContactl.is(":checked")) {
             var nameConcactl = $('.name-contactl');
             nameConcactl.val($(this).val());
         }
    });
    $emailContactl.on("input", function (e) {
        if ($checkboxInvoice.is(":checked")) {
            var emailvoice = $("#email-invoice");
            emailvoice.val($(this).val());
        }
    });
    $checkBoxContactl.change(function () {
        var nameConcactl = $('.name-contactl');
        if ($(this).is(":checked")) {
            nameConcactl.val($firstInput.val());
        }
        else {
            nameConcactl.val('');
        }
    });

    function removeVietnameseTones(str) {
        str = str.normalize("NFD").replace(/[\u0300-\u036f]/g, ""); // Bỏ dấu tổ hợp
        str = str.replace(/đ/g, "d").replace(/Đ/g, "D");
        return str;
    }

    $(".passenger-name-adt, .passenger-name-chd, .passenger-name-inf").on("blur", function () {
        let value = $(this).val().trim();

        if (value !== "") {
            value = removeVietnameseTones(value).toUpperCase();
            $(this).val(value);
        }
    });

    $checkboxInvoice.change(function () {
        if ($(this).is(":checked")) {
            $(".lbl-name-invoice").html('Tên hóa đơn: <span class="color-info">(*)</span>');
            $(".lbl-taxcode-invoice").html('Mã số thuế: <span class="color-info">(*)</span>');
            $(".lbl-add-invoice").html('Địa chỉ hóa đơn: <span class="color-info">(*)</span>');
            $("#name-invoice").addClass('required');
            $("#tax-code-invoice").addClass('required');
            $("#add-invoice").addClass('required');
            if ($("email-invoice").val() === "")
                $("email-invoice").val($('.email-contactl').val());
        }
        else {
            $(".lbl-name-invoice").html('Tên hóa đơn');
            $(".lbl-taxcode-invoice").html('Mã số thuế');
            $(".lbl-add-invoice").html('Địa chỉ hóa đơn');
            $("#name-invoice").removeClass('required');
            $("#tax-code-invoice").removeClass('required');
            $("#add-invoice").removeClass('required');
        }
    });
    $(".toggle-info").click(function () {
        const $btn = $(this);
        const target = $btn.data("target");
        const $info = $(target);
        $info.slideToggle(300, function () {
            $btn.html(
                $info.is(":visible") ?
                    'Rút gọn <svg style="transform: rotate(90deg);" width="8" height="13" viewBox="0 0 8 13" fill="none" xmlns="http://www.w3.org/2000/svg"> <path d="M1.5 1.5L6.5 6.5L1.5 11.5" stroke="#FF3C13" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" /></svg>' :
                    'Xem điều kiện vé  <svg width="8" height="13" viewBox="0 0 8 13" fill="none" xmlns="http://www.w3.org/2000/svg"> <path d="M1.5 1.5L6.5 6.5L1.5 11.5" stroke="#FF3C13" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" /></svg>'
            );
        });
    });
    $('.baggage-out').change(function () {
        updateTotalPriceOutBound();
        
    }); 
    $('.baggage-in').change(function () {
        updateTotalPriceInBound();
    });
    $('#payment-order').click(function () {
        const isAgree = $('#is-agree');
        if (isAgree.is(":checked")) {
            if (validateFormInputs('frmPostOrder') && validateRequiredInputs('.required')
                && validatePassengerBirthDates() && validateEmailInputs() && validatePhoneInputs()
                && validateFullNameInputs() && validatePassengerBirth())
            {
                createOrderBooking($(this));
            }
        }
        else {
            Swal.fire({
                icon: 'warning',
                title: 'Cảnh báo',
                text: 'Bạn chưa đồng ý với điều kiện khi đặt vé!',
                confirmButtonText: 'Đã hiểu'
            }).then((result) => {
                if (result.isConfirmed) {
                    isAgree.focus();
                }
            });
        }
    });
    function updateTotalPriceOutBound() {
        const listBagOut = [];
        let totalPriceBag = 0;
        document.querySelectorAll('.baggage-out').forEach(bag => {
            const selectedOption = bag.options[bag.selectedIndex];
            const value = selectedOption.value;
            const price = selectedOption.getAttribute('data-price');
            const kg = selectedOption.getAttribute('data-kg');
            var bagCheck = listBagOut.find(b => b.idBag == value);
            if (value != 0) {
                if (!bagCheck) {
                    listBagOut.push({
                        idBag: value,
                        totalKg: kg,
                        priceBag: price,
                        countBag: 1
                    })
                }
                else {
                    bagCheck.countBag += 1;
                }
            }
            totalPriceBag += parseInt(price);
        });
        let strBang = '';
        if (listBagOut.length > 0) {
            strBang = 'Mua hành lý:';
            listBagOut.forEach(b => {
                strBang += '(' + b.totalKg + 'Kg x ' + b.countBag + ') ';
            });
        }
        const infoBagOut = document.querySelector('.info-detail .price-service-out p');
        const priceBag = document.querySelector('.info-detail .price-service-out .price');
        const infoTotalPriceOut = document.querySelector('.info-detail p.total-price-out');
        let totalPriceOrder = 0;
        let totalPriceOut = parseInt(infoTotalPriceOut.getAttribute('data-price'));
        let totalPriceOutShow = totalPriceOut + totalPriceBag;
        infoBagOut.innerHTML = 'Dịch vụ mua thêm';
        const newBr = document.createElement('br');
        infoBagOut.appendChild(newBr);
        const newSpan = document.createElement('span');
        newSpan.style.fontWeight = '500';
        newSpan.innerHTML = strBang
        infoBagOut.appendChild(newSpan);
        priceBag.innerHTML = formatNumber(totalPriceBag) + ' VND';
        infoTotalPriceOut.innerHTML = formatNumber(totalPriceOutShow) + ' VND';
        totalPriceOrder += totalPriceOutShow;
        infoTotalPriceOut.setAttribute('data-serviceprice', totalPriceBag);
        const infoTotalPriceIn = document.querySelector('.info-detail p.total-price-in');
        if (infoTotalPriceIn) {
            const totalIn = parseInt(infoTotalPriceIn.getAttribute('data-price'));
            const serviceIn = parseInt(infoTotalPriceIn.getAttribute('data-serviceprice'));
            totalPriceOrder += (totalIn + serviceIn);
        }
        const priceTotal = document.querySelector('.price-total .price-order');
        priceTotal.innerHTML = formatNumber(totalPriceOrder) + ' VND';
    }
    function updateTotalPriceInBound() {
        const listBagIn = [];
        let totalPriceBag = 0;
        document.querySelectorAll('.baggage-in').forEach(bag => {
            const selectedOption = bag.options[bag.selectedIndex];
            const value = selectedOption.value;
            const price = selectedOption.getAttribute('data-price');
            const kg = selectedOption.getAttribute('data-kg');
            var bagCheck = listBagIn.find(b => b.idBag == value);
            if (value != 0) {
                if (!bagCheck) {
                    listBagIn.push({
                        idBag: value,
                        totalKg: kg,
                        priceBag: price,
                        countBag: 1
                    })
                }
                else {
                    bagCheck.countBag += 1;
                }
            }
            totalPriceBag += parseInt(price);
        });
        let strBang = '';
        if (listBagIn.length > 0) {
            strBang = 'Mua hành lý:';
            listBagIn.forEach(b => {
                strBang += '(' + b.totalKg + 'Kg x ' + b.countBag + ') ';
            });
        }
        const infoBagIn = document.querySelector('.info-detail .price-service-in p');
        const priceBag = document.querySelector('.info-detail .price-service-in .price');
        const infoTotalPriceIn = document.querySelector('.info-detail p.total-price-in');
        let totalPriceOrder = 0;
        let totalPriceIn = parseInt(infoTotalPriceIn.getAttribute('data-price'));
        let totalPriceInShow = totalPriceIn + totalPriceBag;
        infoBagIn.innerHTML = 'Dịch vụ mua thêm';
        const newBr = document.createElement('br');
        infoBagIn.appendChild(newBr);
        const newSpan = document.createElement('span');
        newSpan.style.fontWeight = '500';
        newSpan.innerHTML = strBang
        infoBagIn.appendChild(newSpan);
        priceBag.innerHTML = formatNumber(totalPriceBag) + ' VND';
        infoTotalPriceIn.innerHTML = formatNumber(totalPriceInShow) + ' VND';
        totalPriceOrder += totalPriceInShow;
        infoTotalPriceIn.setAttribute('data-serviceprice', totalPriceBag);
        const infoTotalPriceOut = document.querySelector('.info-detail p.total-price-out');
        if (infoTotalPriceOut) {
            const totalOut = parseInt(infoTotalPriceOut.getAttribute('data-price'));
            const serviceOut = parseInt(infoTotalPriceOut.getAttribute('data-serviceprice'));
            totalPriceOrder += (totalOut + serviceOut);
        }
        const priceTotal = document.querySelector('.price-total .price-order');
        priceTotal.innerHTML = formatNumber(totalPriceOrder) + ' VND';
    }
    function validateFormInputs(formId) {
        const form = document.getElementById(formId);
        if (!form) {
            console.error("Không tìm thấy form có ID:", formId);
            return false;
        }

        const inputs = form.querySelectorAll("input[type='text'], textarea");
        for (let input of inputs) {
            const cleanValue = sanitizeInput(input.value);
            if (cleanValue === null)
            {
                Swal.fire({
                    icon: 'warning',
                    title: 'Cảnh báo',
                    text: '"Bạn đã nhập ký tự không được phép!\nVui lòng kiểm tra lại ô: "' + input.value + '!',
                    confirmButtonText: 'Đã hiểu'
                }).then((result) => {
                    if (result.isConfirmed) {
                        input.focus();
                    }
                });
                return false;
            }
            input.value = cleanValue;
        }
        return true;
    }
    function sanitizeInput(text) {
        if (text === null || text === undefined) return "";
        text = String(text).trim().replace(/[\x00-\x1F\x7F]/g, "");
        if (text.length === 0) return "";
        const sqlKeywords = [
            "SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "EXEC", "UNION",
            "ALTER", "CREATE", "TRUNCATE", "MERGE", "REPLACE",
            "--", ";--", ";", "/*", "*/", "@@", "@", "VARCHAR", "NVARCHAR",
            "CAST", "CONVERT", "DECLARE"
        ];

        const upperText = text.toUpperCase();
        for (let keyword of sqlKeywords) {
            const k = keyword.replace(/[-/\\^$*+?.()|[\]{}]/g, "\\$&");
            const pattern = new RegExp("(^|[^A-Z0-9])" + k + "([^A-Z0-9]|$)", "i");
            if (pattern.test(upperText)) {
                return null;
            }
        }
        const safePattern = /^[A-Za-z0-9ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯàáâãèéêìíòóôõùúăđĩũơưĂẮẰẶẲẴÂẦẤẬẨẪÊỀẾỆỂỄÔỒỐỘỔỖƠỜỚỢỞỠƯỪỨỰỬỮỲÝỴỶỸỳýỵỷỹ\u1EA0-\u1EF9\s.,:_\-@()\/!?\+]+$/u;

        if (!safePattern.test(text)) {
            return null;
        }
        if (text.length > 500) text = text.substring(0, 500);

        return text;
    }
    function validateRequiredInputs(selector) {
        let isValid = true;
        document.querySelectorAll(".error-message").forEach(e => e.remove());
        document.querySelectorAll(selector).forEach(el => {
            const value = el.value ? el.value.trim() : "";
            el.style.border = "";
            if (value === "") {
                isValid = false;
                displayWariError(el, "Trường này không được để trống", "error-message");
            }
        });

        return isValid;
    }
    function validatePassengerBirthDates() {
        let isValid = true;
        const dateRegex = /^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/; // dd/mm/yyyy
        document.querySelectorAll(".error-message-date").forEach(e => e.remove());
        const inputs = document.querySelectorAll(
            ".passenger-birth-inf, .passenger-birth-chd, .passenger-birth-adt"
        );
        inputs.forEach(el => {
            const value = el.value.trim();
            el.style.border = "";

            if (value === "") return;

            if (!dateRegex.test(value)) {
                isValid = false;
                displayWariError(el, "Định dạng ngày phải là dd/mm/yyyy", "error-message-date");
            }
        });
        return isValid;
    }
    function validateEmailInputs() {
        let isValid = true;
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        document.querySelectorAll(".error-message-email").forEach(e => e.remove());
        const inputEmails = document.querySelectorAll(".email-contactl, .email-invoice");
        inputEmails.forEach(el => {
            const value = el.value.trim();
            if (value === "") return;
            if (!emailRegex.test(value)) {
                isValid = false;
                displayWariError(el, "Vui lòng nhập đúng định dạng email (ví dụ: hello@domain.vn)", "error-message-email");
            }
        });
        return isValid;
    }

    function validatePhoneInputs() {
        let isValid = true;
        const phoneRegex = /^[0-9]{10}$/;
        document.querySelectorAll(".error-message-phone").forEach(e => e.remove());
        const inputs = document.querySelectorAll(".phone-contactl");
        inputs.forEach(el => {
            const value = el.value.trim();
            if (value === "") return;
            if (!phoneRegex.test(value)) {
                isValid = false;
                displayWariError(el, "Số điện thoại phải gồm 10 chữ số (0–9)", "error-message-phone");
            }
        });

        return isValid;
    }
    function validateFullNameInputs() {
        let isValid = true;
        const nameRegex = /^[A-Za-zÀ-ỹà-ỹ\s]+$/;
        document.querySelectorAll(".error-message-name").forEach(e => e.remove());
        const inputs = document.querySelectorAll(
            ".name-contactl, .passenger-name-inf, .passenger-name-chd, .passenger-name-adt"
        );
        inputs.forEach(el => {
            const value = el.value.trim();
            if (value === "") return;
            if (!nameRegex.test(value)) {
                isValid = false;
                displayWariError(el, "Họ và tên chỉ được chứa chữ cái và khoảng trắng", "error-message-name");
                return;
            }
            const words = value.split(/\s+/);
            if (words.length < 2) {
                isValid = false;
                el.style.border = "2px solid red";
                displayWariError(el, "Vui lòng nhập đầy đủ họ và tên (ít nhất 2 từ)", "error-message-name");
            }
        });
        return isValid;
    }
    function validatePassengerBirth() {
        const depVal = document.querySelector("#departure-date")?.value?.trim();
        const retVal = document.querySelector("#return-date")?.value?.trim();
        if (!depVal && !retVal) return true;
        const refDate = parseDateDDMMYYYY(retVal || depVal);
        if (!refDate) return true;
        let isValid = true;
        document.querySelectorAll(".error-message-birth").forEach(el => el.remove());
        document.querySelectorAll(".passenger-birth-adt, .passenger-birth-chd, .passenger-birth-inf").forEach(input => {
            const value = input.value.trim();
            if (!value) return;

            const birthDate = parseDateDDMMYYYY(value);
            if (!birthDate) return;

            const age = getAge(birthDate, refDate);
            let errorMsg = "";

            if (input.classList.contains("passenger-birth-adt") && age < 12) {
                errorMsg = "Hành khách người lớn phải từ 12 tuổi trở lên.";
            } else if (input.classList.contains("passenger-birth-chd") && (age < 2 || age >= 12)) {
                errorMsg = "Hành khách trẻ em phải từ 2 đến dưới 12 tuổi.";
            } else if (input.classList.contains("passenger-birth-inf") && age >= 2) {
                errorMsg = "Hành khách em bé phải dưới 2 tuổi.";
            }

            if (errorMsg) {
                isValid = false;
                displayWariError(input, errorMsg, "error-message-birth");
            } else {
                input.style.border = "";
            }
        });

        return isValid;
    }
    function parseDateDDMMYYYY(str) {
        const parts = str.split("/");
        if (parts.length !== 3) return null;
        const day = parseInt(parts[0], 10);
        const month = parseInt(parts[1], 10) - 1;
        const year = parseInt(parts[2], 10);
        const date = new Date(year, month, day);
        return isNaN(date.getTime()) ? null : date;
    }
    function getAge(birthDate, refDate) {
        let age = refDate.getFullYear() - birthDate.getFullYear();
        const m = refDate.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && refDate.getDate() < birthDate.getDate())) {
            age--;
        }
        return age;
    }
    function displayWariError(el, text, className) {
        el.style.border = "";
        el.style.border = "1px solid #e82d2d";
        const error = document.createElement("div");
        error.className = className;
        error.style.color = "#e82d2d";
        error.style.fontSize = "12px";
        error.style.marginTop = "4px";
        error.textContent = text;
        const parentDiv = el.closest(".form-custom");
        if (parentDiv) {
            parentDiv.insertAdjacentElement("afterend", error);
        } else {
            el.insertAdjacentElement("afterend", error);
        }
    }
    function createOrderBooking(linkOrderElem) {
        let listFlight = [];
        let listPassger = [];
        const typesPax = ["adt", "chd", "inf"];
        let session = document.getElementById('frmPostOrder').getAttribute('data-session');
        document.querySelectorAll(".item-ticket").forEach(item => {
            listFlight.push({
                bookingKey: item.getAttribute("data-booking-code"),
                airCode: item.getAttribute("data-airline"),
                wayType: item.getAttribute("data-waytype")
            })
        });
        let paxId = 0;
        typesPax.forEach(type => {
            const genders = document.querySelectorAll(`#passger-info .passenger-gender-${type}`);
            const names = document.querySelectorAll(`#passger-info .passenger-name-${type}`);
            const cardNums = document.querySelectorAll(`#passger-info .passenger-cardnum-${type}`);
            const births = document.querySelectorAll(`#passger-info .passenger-birth-${type}`);
            const count = names.length;
            for (let i = 0; i < count; i++) {
                const genderEl = genders[i];
                const nameEl = names[i];
                const cardNumEl = cardNums[i];
                const birthEl = births[i];
                const genderValue = genderEl ? genderEl.value : "1";
                const nameValue = nameEl ? nameEl.value.trim() : "";
                const cardNumValue = cardNumEl ? cardNumEl.value.trim() : "";
                const birthValue = birthEl ? birthEl.value.trim() : "";
                const baggageSelectOut = document.querySelectorAll(`.baggage-out`)[paxId];
                const baggageSelectIn = document.querySelectorAll(`.baggage-in`)[paxId];
                let bagOutId = "0";
                let totalOutKg = "0";
                let bagInId = "0";
                let totalInKg = "0";

                if (baggageSelectOut) {
                    const selectedOptionOut = baggageSelectOut.options[baggageSelectOut.selectedIndex];
                    bagOutId = selectedOptionOut.value;
                    totalOutKg = selectedOptionOut.getAttribute("data-kg") || "0";
                }
                if (baggageSelectIn) {
                    const selectedOptionIn = baggageSelectIn.options[baggageSelectIn.selectedIndex];
                    bagInId = selectedOptionIn.value;
                    totalInKg = selectedOptionIn.getAttribute("data-kg") || "0";
                }
                listPassger.push({
                    genderPax: genderValue,
                    namePax: nameValue,
                    cardNum: cardNumValue,
                    birthPax: birthValue,
                    typePax: type,
                    bagIdOut: bagOutId,
                    totalKgOut: totalOutKg,
                    bagIdIn: bagInId,
                    totalKgIn: totalInKg
                });
                paxId++;
            }
        });
        let concactl = {
            titleContactl: document.querySelector("#contaclt-info .title-contactl").value,
            nameContactl: document.querySelector("#contaclt-info .name-contactl").value,
            phoneContactl: document.querySelector("#contaclt-info .phone-contactl").value,
            emailContactl: document.querySelector("#contaclt-info .email-contactl").value,
            addContactl: document.querySelector("#contaclt-info .add-contactl").value,
            otherRequirementsContactl: document.querySelector("#contaclt-info .other-requirements").value,
        }
        let invoice = {
            isInvoice: document.getElementById("is-invoice").checked,
            nameInvoice: document.getElementById("name-invoice").value,
            taxCodeInvoice: document.getElementById("tax-code-invoice").value,
            emailInvoice: document.getElementById("email-invoice").value,
            addInvoice: document.getElementById("add-invoice").value,
        }
        var listBooking= {
            sessionId: session,
            flightList: listFlight,
            pasgerList: listPassger,
            contaclOrder: concactl,
            invoiceOrder: invoice
        };
        var datapost = {
            __RequestVerificationToken: document.querySelector('input[name="__RequestVerificationToken"]').value,
            request: listBooking
        }
        var $link = linkOrderElem instanceof jQuery ? linkOrderElem : $(linkOrderElem);
        if ($link.data('processing')) return;
        $link.data('processing', true);
        var startTime = Date.now();
        var originalHtml = $link.html();
        $link.data('original-html', originalHtml);

        // Disable UI (mô phỏng disabled cho <a>)
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
            url: '/Checkcreatebooking',
            method: 'POST',
            data: datapost,
            success: function (response) {
                if (response.status && response.dataResult) {
                    var elapsed = Date.now() - startTime;
                    var remaining = 3000 - elapsed;
                    if (remaining < 0) remaining = 0;
                    setTimeout(function () {
                        window.location.href = '/Thanhtoandonhang?data=' + encodeURIComponent(response.dataResult);
                    }, remaining);
                } else {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Cảnh báo',
                        text: 'Xảy ra lỗi khi đặt vé!',
                        confirmButtonText: 'Đã hiểu'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $link.removeClass('disabled-link');
                        }
                    });
                }
            },
            error: function (xhr, status, error) {
                console.error("Lỗi: ", error);
                Swal.fire({
                    icon: 'warning',
                    title: 'Cảnh báo',
                    text: 'Xảy ra lỗi khi đặt vé!',
                    confirmButtonText: 'Đã hiểu'
                }).then((result) => {
                    if (result.isConfirmed) {
                        $link.removeClass('disabled-link');
                    }
                });
            }
        });
    }
})