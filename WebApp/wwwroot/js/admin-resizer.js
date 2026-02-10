document.addEventListener('DOMContentLoaded', function () {
    const resizer = document.getElementById('dragMe');
    const leftSide = document.querySelector('.admin-sidemenu-wapper');
    const container = document.querySelector('.notebook-horizontal');

    if (!resizer || !leftSide) return;

    resizer.addEventListener('mousedown', function (e) {
        e.preventDefault(); 

        const startX = e.clientX;
        const startWidth = leftSide.getBoundingClientRect().width;
        const totalWidth = container.offsetWidth;

        const onMouseMove = function (e) {
            let newWidth = startWidth + (e.clientX - startX);

            
            const minLeft = totalWidth * 0.1; 
            const maxLeft = totalWidth * 0.5; 

            
            if (newWidth < minLeft) newWidth = minLeft;
            if (newWidth > maxLeft) newWidth = maxLeft;

            leftSide.style.flex = `0 0 ${newWidth}px`;
            leftSide.style.width = `${newWidth}px`;
        };

        const onMouseUp = function () {
            document.removeEventListener('mousemove', onMouseMove);
            document.removeEventListener('mouseup', onMouseUp);
            document.body.style.cursor = 'default';
        };

        document.addEventListener('mousemove', onMouseMove);
        document.addEventListener('mouseup', onMouseUp);
        document.body.style.cursor = 'col-resize';
    });
});