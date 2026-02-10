document.addEventListener("DOMContentLoaded", function () {
    // 1. Знаходимо наш хедер
    const header = document.querySelector(".header-supscription-plans-admin");

    // 2. ВАЖЛИВО: Тут вкажіть клас того блоку з _Layout, який має скрол!
    // Наприклад, це може бути .main-content, .wrapper або body
    const scrollContainer = document.querySelector(".main-container") || window;

    let lastScrollTop = 0;

    scrollContainer.addEventListener("scroll", function () {
        // Отримуємо позицію скролу елемента
        let scrollTop = this.scrollTop || document.documentElement.scrollTop;

        // Логіка ховання
        if (scrollTop > lastScrollTop && scrollTop > 50) {
            // Скрол вниз -> ховаємо
            header.classList.add("header-hidden");
        } else {
            // Скрол вверх -> показуємо
            header.classList.remove("header-hidden");
        }

        // Запобігаємо від'ємним значенням (для Safari/Mobile)
        if (scrollTop < 0) scrollTop = 0;

        lastScrollTop = scrollTop;
    });
});