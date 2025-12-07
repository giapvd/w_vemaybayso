
$(document).ready(function () {
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
    var $checkBoxContactl = $("#is-chose-info-contactl");
    var $firstInput = $("input.passenger-name-adt").first();
    var $checkboxInvoice = $('#is-invoice');
    var $emailContactl = $(".email-contactl");
    if ($firstInput.length === 0) return;

    $checkBoxContactl.on("mousedown", function (e) {
        if (this.checked) {
            this.isAlreadyChecked = true;
        } else {
            this.isAlreadyChecked = false;
        }
    });

    $checkBoxContactl.on("click", function (e) {
        if (this.isAlreadyChecked) {
            this.checked = false;
        }

    });
    $checkboxInvoice.on("mousedown", function (e) {
        if (this.checked) {
            this.isAlreadyChecked = true;
        } else {
            this.isAlreadyChecked = false;
        }
    });

    $checkboxInvoice.on("click", function (e) {
        if (this.isAlreadyChecked) {
            this.checked = false;
        }
        changeSelectInvoice();
    });
    $firstInput.on("input", function (e) {
        if ($checkBoxContactl.is(":checked")) {
             var nameConcactl = $('.name-contactl');
             nameConcactl.val($(this).val());
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
    const planeItemsSelected = document.querySelectorAll(".plane-items-selected");
    planeItemsSelected.forEach((planeItemSelected) => {
        planeItemSelected.querySelector(".clicked").addEventListener("click", () => {
            planeItemSelected.querySelector(".conditions").classList.toggle("hidden");
        });
    });
    
    $(".passenger-name-adt, .passenger-name-chd, .passenger-name-inf").on("blur", function () {
        let value = $(this).val().trim();
        if (value !== "") {
            value = removeVietnameseTones(value).toUpperCase();
            $(this).val(value);
        }
    });
    function changeSelectInvoice() {
        if ($checkboxInvoice.is(":checked")) {
            $(".lbl-name-invoice").html('Tên hóa đơn: <span class="text-red-500">*</span><input type="text" placeholder="Ví dụ: Vé máy bay số" class="outline-none w-full name-invoice required" id="name-invoice"/>');
            $(".lbl-taxcode-invoice").html('Mã số thuế: <span class="text-red-500">*</span><input type="text" placeholder="Ví dụ: 0123456789" class="tax-code-invoice outline-none w-full required" id="tax-code-invoice"/>');
            $(".lbl-add-invoice").html('Địa chỉ hóa đơn: <span class="text-red-500">*</span><input type="text" placeholder="Số 7 Ngõ 4 P. Đào Hinh, Việt Hưng, Long Biên, Hà Nội." class="add-invoice outline-none w-full required" id="add-invoice"/>');
        }
        else {
            $(".lbl-name-invoice").html('Tên hóa đơn: <input type="text" placeholder="Ví dụ: Vé máy bay số" class="outline-none w-full name-invoice" id="name-invoice"/>');
            $(".lbl-taxcode-invoice").html('Mã số thuế: <input type="text" placeholder="Ví dụ: 0123456789" class="tax-code-invoice outline-none w-full" id="tax-code-invoice"/>');
            $(".lbl-add-invoice").html('Địa chỉ hóa đơn: <span class="text-red-500">*</span><input type="text" placeholder="Số 7 Ngõ 4 P. Đào Hinh, Việt Hưng, Long Biên, Hà Nội." class="add-invoice outline-none w-full" id="add-invoice"/>');
        }
    }
    
    $('#create-order').click(function () {
        if (validateFormInputs('frmPostOrder') && validateRequiredInputs('.required')
                && validatePassengerBirthDates() && validateEmailInputs() && validatePhoneInputs()
                && validateFullNameInputs() && validatePassengerBirth())
         {
                createOrderBooking($(this));
         }
        else {
            return;
        }
    });
    
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
        document.querySelectorAll(selector).forEach(el => {
            const value = el.value ? el.value.trim() : "";
            el.style.border = "";
            if (value === "") {
                isValid = false;
                displayWariError(el, "Không được rỗng");
            }
            else {
                if (el.classList.contains("selectd-type")) {
                    const tagDiv = el.parentElement;
                    const tagLabel = tagDiv.parentElement;
                    const tagDivParent = tagLabel.parentElement;
                    const error = tagDivParent.querySelector(".w-fit");
                    if(error) error.remove();
                    tagLabel.classList.remove("border-red");
                    tagLabel.classList.add("border-[#B3B0D8]");
                    tagDivParent.classList.remove("text-red-500");
                }
                else {
                    const tagLabel = el.parentElement;
                    const tagDiv = tagLabel.parentElement;
                    const error = tagDiv.querySelector(".error-msg");
                    if (error) error.remove();
                    tagLabel.classList.remove("border-red");
                    tagLabel.classList.add("border-[#B3B0D8]");
                    tagDiv.classList.remove("text-red-500");
                }
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
                displayWariError(el, "Sai dd/mm/yyyy");
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
                displayWariError(el, "Không đúng định dạng email (ví dụ: hello@domain.vn)");
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
                displayWariError(el, "Điện thoại phải gồm 10 chữ số (0–9)");
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
                displayWariError(el, "Ký tự vi phạm");
                return;
            }
            const words = value.split(/\s+/);
            if (words.length < 2) {
                isValid = false;
                displayWariError(el, "ít nhất 2 từ");
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
                errorMsg = "Người lớn >= 12 tuổi";
            } else if (input.classList.contains("passenger-birth-chd") && (age < 2 || age >= 12)) {
                errorMsg = "2 <= Trẻ em < 12.";
            } else if (input.classList.contains("passenger-birth-inf") && age >= 2) {
                errorMsg = "Em bé < 2 tuổi.";
            }

            if (errorMsg) {
                isValid = false;
                displayWariError(input, errorMsg);
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
    function displayWariError(el, text) {
        if (el.classList.contains("selectd-type")) {
            const tagDiv = el.parentElement;
            const tagLabel = tagDiv.parentElement;
            const tagDivParent = tagLabel.parentElement;
            const tagspan = document.createElement("span");
            const spancheck = tagDivParent.querySelector('span.w-fit');
            if (!spancheck) {
                tagspan.className = "w-fit ml-4";
                tagspan.textContent = text;
                tagDivParent.appendChild(tagspan);
                tagLabel.classList.remove("border-[#B3B0D8]");
                tagLabel.classList.add("border-red");
                tagDivParent.classList.add("text-red-500");
            }
        }
        else {
            const tagLabel = el.parentElement;
            const tagDiv = tagLabel.parentElement;
            const tagspan = document.createElement("span");
            const spancheck = tagDiv.querySelector('span.error-msg');
            if (!spancheck) {
                tagspan.className = "w-fit ml-4 text-red-500 error-msg";
                tagspan.textContent = text;
                tagDiv.appendChild(tagspan);
                tagLabel.classList.remove("border-[#B3B0D8]");
                tagLabel.classList.add("border-red");
                tagDiv.classList.add("text-red-500");
            }
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
            const memberNum = document.querySelectorAll(`#passger-info .cardnum-member-${type}`);
            const count = names.length;
            for (let i = 0; i < count; i++) {
                const genderEl = genders[i];
                const nameEl = names[i];
                const cardNumEl = cardNums[i];
                const birthEl = births[i];
                const memberNumEl = memberNum[i]
                const genderValue = genderEl ? genderEl.value : "1";
                const nameValue = nameEl ? nameEl.value.trim() : "";
                const cardNumValue = cardNumEl ? cardNumEl.value.trim() : "";
                const birthValue = birthEl ? birthEl.value.trim() : "";
                const memberNumValue = memberNumEl ? memberNumEl.value.trim() : "";
                let bagOutId = "0";
                let totalOutKg = "0";
                let bagInId = "0";
                let totalInKg = "0";
                listPassger.push({
                    genderPax: genderValue,
                    namePax: nameValue,
                    cardNum: cardNumValue,
                    birthPax: birthValue,
                    memberNum: memberNumValue,
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
            otherRequirementsContactl: document.querySelector("#other-requirements").value,
        }
        let invoice = {
            isInvoice: document.getElementById("is-invoice").checked,
            nameInvoice: document.getElementById("name-invoice").value,
            taxCodeInvoice: document.getElementById("tax-code-invoice").value,
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