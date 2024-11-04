function adjustTextSize(size) {
    document.body.style.fontSize = size + 'px';
}

function toggleHighContrastMode() {
    document.body.classList.toggle('high-contrast');
}

function toggleLargeCursor() {
    document.body.classList.toggle('large-cursor');
}

function scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('increase-text').onclick = () => adjustTextSize(20);
    document.getElementById('decrease-text').onclick = () => adjustTextSize(14);
    document.getElementById('toggle-contrast').onclick = toggleHighContrastMode;
    document.getElementById('toggle-cursor').onclick = toggleLargeCursor;
    document.getElementById('scroll-top').onclick = scrollToTop;
});