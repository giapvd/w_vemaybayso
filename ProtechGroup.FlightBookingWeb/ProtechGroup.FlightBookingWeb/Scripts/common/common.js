let a = 1
// open model
const modal = document.getElementById('modal')
const modal2 = document.getElementById('modal2')
const modal3 = document.getElementById('modal3')
// Mở modal
function openModal() {
    modal.style.display = 'flex'
}
// Mở modal
let isArrCheck = []

function openModal2(numberChang, name) {
    //numberChang: số chặng
    // name : 1 : start hoặc 2: end
    isArrCheck.push(numberChang)
    isArrCheck.push(name)
    modal2.style.display = 'flex'
    const listItems = document.querySelectorAll('.list-items li')
    listItems.forEach(li => {
        li.style.display = 'list-item'
    })
}



function filterAirports() {
    const input = document.getElementById('airportInput')
    const filter = input.value.toLowerCase()
    const listItems = document.querySelectorAll('.list-items li')
    listItems.forEach(li => {
        const text = li.textContent.toLowerCase()
        if (text.includes(filter)) {
            li.style.display = 'list-item'
        } else {
            li.style.display = 'none'
        }
    })
}

function filterAirports2() {
    const input = document.getElementById('airportInput2')
    const filter = input.value.toLowerCase() // chữ thường để so sánh không phân biệt hoa thường
    const listItems = document.querySelectorAll('.list-items li')
    listItems.forEach(li => {
        const text = li.textContent.toLowerCase()
        if (text.includes(filter)) {
            li.style.display = 'list-item'
        } else {
            li.style.display = 'none'
        }
    })
}

// Đóng modal
for (let i = 1; i <= 3; i++) {
    const minus = document.querySelector(`.minus${i}`)
    const plus = document.querySelector(`.plus${i}`)
    const count = document.querySelector(`.count${i}`)

    const member = document.querySelector(`#member${i}`)
    const memberModal = document.querySelector(`#memberModal${i}`)
    if (minus) {
        minus.addEventListener('click', () => {
            let value = parseInt(count.value) || 0
            if (value > 0) {
                count.value = value - 1
                member.textContent = count.value
                memberModal.textContent = count.value
            }
        })
    }
    if (plus) {
        plus.addEventListener('click', () => {
            let value = parseInt(count.value) || 0
            count.value = value + 1
            member.textContent = count.value
            memberModal.textContent = count.value
        })
    }
    
}
// Gán flatpickr cho tất cả input có class .calendar-input
function startFlatpickr() {
    flatpickr('.calendar-input', {
        dateFormat: 'd/m/Y',
        minDate: 'today',
        locale: 'vn'
        // Bạn có thể thêm showMonths, range mode, v.v. nếu muốn
    })
}
startFlatpickr()
const radiosSearch = document.querySelectorAll('.typeItinerary')
const iconOne = document.querySelector('.icon-one')
const iconTwo = document.querySelector('.icon-two')
const dateEnd = document.querySelector('.dateEnd')


function removeRoute(event) {
    // Ngăn sự kiện nổi bọt nếu cần
    event?.stopPropagation()
    // Tìm phần tử cần xoá (cha chứa class item-route)
    const routeItem = event.target.closest('.item-route')
    if (routeItem) {
        routeItem.remove()
    }

    const indexItemaddRemove =
        document.querySelectorAll('.item-route').length - 1
    if (indexItemaddRemove >= 2) {
        const item = document
            .querySelectorAll('.item-route')
        [indexItemaddRemove].querySelector('.btn-remove')
        const icon = document.createElement('i')
        icon.className = 'color-info fa-solid fa-trash remove-route'
        item.appendChild(icon)
    }
}
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
function updateIcon() {
    const roundtrip = document.getElementById('roundtrip')
    if (roundtrip) {
        if (roundtrip.checked) {
            document.querySelector('.route-point').classList.remove('col-8');
            document.querySelector('.date-search').classList.remove('col-4');
            document.querySelector('.date-search').classList.add('col-6');
            document.querySelector('.startDay').classList.remove('col-12');
            iconOne.classList.add('hidden');
            iconTwo.classList.remove('hidden');
            dateEnd.classList.remove('hidden');
            $('#roundType').val(1);
        } else {
            document.querySelector('.route-point').classList.add('col-8');
            document.querySelector('.date-search').classList.remove('col-6');
            document.querySelector('.date-search').classList.add('col-4');
            document.querySelector('.startDay').classList.add('col-12');
            iconOne.classList.remove('hidden');
            iconTwo.classList.add('hidden');
            dateEnd.classList.add('hidden');
            $('#roundType').val(0);
        }
    }
}
radiosSearch.forEach(radio => {
    radio.addEventListener('change', updateIcon)
})
window.addEventListener('DOMContentLoaded', updateIcon)
// MENU
const menuButton = document.querySelector('.openMenu')
const navigation = document.querySelector('.navigation')
// Mở/đóng menu
menuButton.addEventListener('click', function () {
    navigation.classList.toggle('openMenu')
    if (navigation.classList.contains('openMenu')) {
        document.body.classList.add('no-scroll')
    } else {
        document.body.classList.remove('no-scroll')
    }
})
// Click ra ngoài để đóng menu
document.addEventListener('click', function (event) {
    if (
        !navigation.contains(event.target) &&
        !menuButton.contains(event.target)
    ) {
        navigation.classList.remove('openMenu')
        document.body.classList.remove('no-scroll')
    }
})
// Toggle submenu
document.addEventListener('click', e => {
    if (e.target.classList.contains('toggle-submenu')) {
        const nextUl = e.target.nextElementSibling
        if (nextUl && nextUl.tagName === 'UL') {
            nextUl.classList.toggle('open')
        }
    }
})
if (window.innerWidth < 1207) {
    console.log(window.innerWidth)
    document.querySelectorAll('li').forEach(li => {
        const submenu = li.querySelector('.sub-menu')
        if (submenu) {
            const toggle = document.createElement('span')
            toggle.classList.add('toggle-submenu')
            toggle.textContent = '▼'
            li.insertBefore(toggle, submenu)
        }
    })
}
// END MENU
// backToTop
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
        scrollToTop(1000) // Thời gian: 1000ms = 1 giây (bạn có thể tăng lên)
    })

    function scrollToTop(duration) {
        const start = window.scrollY
        const startTime = performance.now()

        function animate(currentTime) {
            const elapsed = currentTime - startTime
            const progress = Math.min(elapsed / duration, 1) // [0..1]
            const ease = 1 - Math.pow(1 - progress, 3) // ease-out cubic
            window.scrollTo(0, start * (1 - ease))
            if (elapsed < duration) {
                requestAnimationFrame(animate)
            }
        }
        requestAnimationFrame(animate)
    }
})