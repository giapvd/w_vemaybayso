$(document).ready(function () {
    $('.baggage-out').change(function () {
        updateTotalPriceOutBound();

    });
    $('.baggage-in').change(function () {
        updateTotalPriceInBound();
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
})