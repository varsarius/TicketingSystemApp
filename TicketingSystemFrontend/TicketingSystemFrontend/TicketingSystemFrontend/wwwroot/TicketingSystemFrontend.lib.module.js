export function beforeWebStart(options) {
    // Создаем элемент с анимацией загрузки
    const loadingDiv = document.createElement('div');
    loadingDiv.id = 'loading-screen';
    loadingDiv.style.position = 'fixed';
    loadingDiv.style.top = '0';
    loadingDiv.style.left = '0';
    loadingDiv.style.width = '100%';
    loadingDiv.style.height = '100%';
    loadingDiv.style.backgroundColor = 'white'; // Или любой фон
    loadingDiv.style.display = 'flex';
    loadingDiv.style.justifyContent = 'center';
    loadingDiv.style.alignItems = 'center';
    loadingDiv.style.zIndex = '9999';

    // Добавляем спиннер (простая CSS-анимация)
    const spinner = document.createElement('div');
    spinner.style.border = '16px solid #f3f3f3'; // Светлый бордер
    spinner.style.borderTop = '16px solid #3498db'; // Синий цвет (можно изменить)
    spinner.style.borderRadius = '50%';
    spinner.style.width = '120px';
    spinner.style.height = '120px';
    spinner.style.animation = 'spin 1s linear infinite';

    loadingDiv.appendChild(spinner);

    // Добавляем keyframes для анимации
    const style = document.createElement('style');
    style.innerHTML = `
        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
    `;
    document.head.appendChild(style);

    // Добавляем в body
    document.body.appendChild(loadingDiv);
}

export function afterWebAssemblyStarted(blazor) {
    // Удаляем элемент загрузки после завершения
    const loadingDiv = document.getElementById('loading-screen');
    if (loadingDiv) {
        loadingDiv.remove();
    }
}