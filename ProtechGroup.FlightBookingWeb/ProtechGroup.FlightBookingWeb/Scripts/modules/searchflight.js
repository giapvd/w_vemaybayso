$(document).ready(function () {
    $("#airportInput").keyup(function () {
        var keyword = $(this).val();
        if (keyword.length > 0) {
            $("#airportResults").empty();
            var queryString = encodeURIComponent(keyword);
            $.ajax({
                url: '/Kiemtrathongtinsanbay',
                type: 'POST',
                data: {
                    __RequestVerificationToken: document.querySelector('input[name="__RequestVerificationToken"]').value,
                    plainText: queryString
                },
                success: function (encryptedQuery) {
                    $.ajax({
                        url: '/Timkiemsanbay/' + encodeURIComponent(encryptedQuery),
                        type: 'GET',
                        success: function (data) {
                            if (data.length > 0) {
                                airportsSearch = [];
                                $.each(data, function (i, item) {
                                    airportsSearch.push(
                                        {
                                            code: item.AirportCode,
                                            cityname: item.CityName
                                        }
                                    );
                                });
                                addAirport("search-airport");
                            } else {
                                airportsSearch = [];
                                addAirport("search-airport");
                            }
                        }
                    });
                }
            });
        } else {
            addAirport("vietnam");
        }
    });
    $('#btn-search').on('click', async function (e) {
        e.preventDefault();
        const isValid = await checkValidateInputSearch();
        if (isValid) {
            var $btn = $(this);
            $btn.prop('disabled', true);
            var originalText = $btn.html();
            $btn.css("display", "flex");
            $btn.css("flex-flow", "row");
            $btn.css("justify-content", "center");
            $btn.css("align-items", "center");
            $btn.html('<img src="/Content/img/loading.gif" style="width:30px;height:30px;vertical-align:middle;margin-right:5px;margin-bottom: 0;" /> Tìm kiếm...');
            var token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            var data = {
                __RequestVerificationToken: token,
                departure: $("#departure-point-value").val().trim(),
                arrival: $("#arrival-point-value").val().trim(),
                departureDate: $("#departure-date-value").val().trim(),
                returnDate: $("#return-date-value").val().trim() || null,
                roundType: parseInt($("#round_type-value").val().trim()),
                countAdt: parseInt($("#adult_passenger").val().trim()),
                countChd: parseInt($("#child_passenger").val().trim()),
                countInf: parseInt($("#infant_passenger").val().trim())
            };

            var startTime = new Date().getTime();
            $.ajax({
                url: '/Kiemtrathongtintimkiemchuyenbay',
                type: 'POST',
                data: data,
                success: function (response) {
                    if (response && response.dataResult) {
                        var elapsed = new Date().getTime() - startTime;
                        var remaining = 3000 - elapsed; 
                        if (remaining < 0) remaining = 0;
                        setTimeout(function () {
                            postRedirect("/Ketquatimkiem", {
                                __RequestVerificationToken: token,
                                data: response.dataResult
                            });
                        }, remaining);
                    } else {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Cảnh báo',
                            text: 'Xảy ra lỗi tìm chuyến bay!',
                            confirmButtonText: 'Đã hiểu'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $btn.prop('disabled', false).html(originalText);
                            }
                        });
                        
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Cảnh báo',
                        text: 'Xảy ra lỗi tìm chuyến bay!',
                        confirmButtonText: 'Đã hiểu'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $btn.prop('disabled', false).html(originalText);
                        }
                    });
                }
            });
        }
    });
});
async function checkValidateInputSearch() {
    let departure = $("#departure-point-value").val().trim();
    let arrival = $("#arrival-point-value").val().trim();
    let departureDate = $("#departure-date-value").val().trim();
    let returnDate = $("#return-date-value").val().trim();
    let roundType = parseInt($("#round_type-value").val().trim());
    let countAdt = parseInt($("#adult_passenger").val().trim());
    let countChd = parseInt($("#child_passenger").val().trim());
    let countInf = parseInt($("#infant_passenger").val().trim());
    if (departure === "") {
        await Swal.fire({
            icon: 'warning',
            title: 'Cảnh báo',
            text: "Bạn chưa chọn điểm đi!",
            confirmButtonText: 'Đã hiểu'
        });
        return false;
    }
    if (arrival === "") {
        await Swal.fire({
            icon: 'warning',
            title: 'Cảnh báo',
            text: "Bạn chưa chọn điểm đến!",
            confirmButtonText: 'Đã hiểu'
        });
        return false;
    }
    
    if (departure === arrival) {
        await Swal.fire({
            icon: 'warning',
            title: 'Cảnh báo',
            text: "Điểm đến không được trùng điểm đi!",
            confirmButtonText: 'Đã hiểu'
        });
        return false;
    }

    if (departureDate === "") {
        await Swal.fire({
            icon: 'warning',
            title: 'Cảnh báo',
            text: "Bạn chưa chọn ngày đi!",
            confirmButtonText: 'Đã hiểu'
        });
        return false;
    }
    else
    {
        if (!isValidDate(departureDate)) {
            await Swal.fire({
                icon: 'warning',
                title: 'Cảnh báo',
                text: "Không đúng định dạng dd/mm/yyyy!",
                confirmButtonText: 'Đã hiểu'
            });
            return false;
        }
    }
    if (roundType == 1) {
        if (returnDate === "") {
            await Swal.fire({
                icon: 'warning',
                title: 'Cảnh báo',
                text: "Bạn chưa chọn ngày về!",
                confirmButtonText: 'Đã hiểu'
            });
            return false;
        }
        else {
            if (isValidDate(returnDate)) {
                if (!isValidDateRange(departureDate, returnDate)) {
                    await Swal.fire({
                        icon: 'warning',
                        title: 'Cảnh báo',
                        text: "Ngày về phải lớn hơn ngày đi!",
                        confirmButtonText: 'Đã hiểu'
                    });
                    return false;
                }
            }
            else {
                await Swal.fire({
                    icon: 'warning',
                    title: 'Cảnh báo',
                    text: "Không đúng định dạng dd/mm/yyyy!",
                    confirmButtonText: 'Đã hiểu'
                });
                return false;
            }
        }
    }
    let totalPax = countAdt + countChd + countInf;
    if (totalPax > 9) {
        await Swal.fire({
            icon: 'warning',
            title: 'Cảnh báo',
            text: "Hành khách không quá 9 người!",
            confirmButtonText: 'Đã hiểu'
        });
        return false;
    }
    else {
        if (totalPax == 0) {
            await Swal.fire({
                icon: 'warning',
                title: 'Cảnh báo',
                text: "Số lượng hành khách phải lớn hơn 0!",
                confirmButtonText: 'Đã hiểu'
            });
            return false;
        }
        else {
            if (countInf > countAdt) {
                await Swal.fire({
                    icon: 'warning',
                    title: 'Cảnh báo',
                    text: "Số lượng em bé không được lớn hơn người lớn!",
                    confirmButtonText: 'Đã hiểu'
                });
                return false;
            }
        }
    }
    return true;
}
function checkRoute(elementClick, code) {
    modal2.style.display = 'none'
    const elements = document.querySelectorAll('.point-route')
    elements.forEach(el => {
        if (
            isArrCheck[0] == el.getAttribute('data-position-box') &&
            isArrCheck[1] == el.getAttribute('data-position-item')
        ) {
            el.textContent = elementClick.textContent
            el.nextElementSibling.value = code
        }
    })
    isArrCheck = []
}