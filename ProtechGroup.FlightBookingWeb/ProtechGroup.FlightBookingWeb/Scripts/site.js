(function () {
    const slides = document.querySelectorAll('#slider .slide');
    let current = 0;
    function showSlide(idx) {
        slides.forEach((slide, i) => {
            slide.style.display = i === idx ? 'block' : 'none';
        });
    }
    document.getElementById('prevSlide').addEventListener('click', function () {
        current = (current - 1 + slides.length) % slides.length;
        showSlide(current);
    });
    document.getElementById('nextSlide').addEventListener('click', function () {
        current = (current + 1) % slides.length;
        showSlide(current);
    });
    let autoSlide = setInterval(() => {
        current = (current + 1) % slides.length;
        showSlide(current);
    }, 3000);
    // Pause on mouse enter
    document.getElementById('slider').addEventListener('mouseenter', () => {
        clearInterval(autoSlide);
    });
    // Resume on mouse leave
    document.getElementById('slider').addEventListener('mouseleave', () => {
        autoSlide = setInterval(() => {
            current = (current + 1) % slides.length;
            showSlide(current);
        }, 4000);
    });
    // Initial display
    showSlide(current);
})();