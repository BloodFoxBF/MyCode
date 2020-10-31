const logout = document.querySelector('.button-logout');
const arrow = document.querySelector('.arrow');
const cardButton = document.querySelector('.card-button');
const sectionMarks = document.querySelector('.sections-marks');
const sectionTasks = document.querySelector('.sections-tasks');
const arrowDown = document.querySelector('.arrowDown');
const solve = document.querySelector('.solve');

if (document.location.pathname == '/Frontend/firstStudent.html') {

    function logoutFrom() {
        document.location.href = 'index.html';
    };
    
    function goToMarks (event) {
        const target = event.target;
        const Marks = target.closest('.sections-marks');
        if (Marks) {
            document.location.href = 'myMarks.html';
        }
    };

    function goToTasks (event) {
        const target = event.target;
        const Marks = target.closest('.sections-tasks');
        if (Marks) {
            document.location.href = 'tasks.html';
        }
    }

    sectionMarks.addEventListener('click', goToMarks);
    logout.addEventListener('click', logoutFrom);
    sectionTasks.addEventListener('click', goToTasks);
}

if (document.location.pathname == '/Frontend/myMarks.html') {
    
    function goToTask () {
        document.location.href = 'aboutTask.html';
    };

    function goToBack () {
        document.location.href = 'firstStudent.html';
    };
    
    arrow.addEventListener('click', goToBack);
    cardButton.addEventListener('click', goToTask);
}

if (document.location.pathname == '/Frontend/tasks.html') {
    
    function goToSolve () {
        document.location.href = 'solveTask.html';
    }

    function goToBack () {
        document.location.href = 'firstStudent.html';
    };
    
    //arrowDown.addEventListener('click', downBlock);
    arrow.addEventListener('click', goToBack);
    solve.addEventListener('click', goToSolve);
}
