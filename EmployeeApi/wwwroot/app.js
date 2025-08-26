
const API_BASE_URL = 'https://localhost:7056';

let employees = [];
let editingId = null;
let deleteId = null;

let employeeModal;
let deleteModal;
let viewEmployeeModal;

document.addEventListener('DOMContentLoaded', () => {
    employeeModal = new bootstrap.Modal(document.getElementById('employeeModal'));
    deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
    viewEmployeeModal = new bootstrap.Modal(document.getElementById('viewEmployeeModal'));
    loadEmployees();
});

// alerts
function showMessage(text, isError = false) {
    Swal.fire({
        icon: isError ? 'error' : 'success',
        title: text,
        timer: 2000,
        showConfirmButton: false,
        position: 'top-end',
        toast: true
    });
}


// Get all employees from server
async function loadEmployees() {
    try {
        const response = await fetch(`${API_BASE_URL}/employees`);
        employees = await response.json();
        showEmployeesTable();
    } catch (error) {
        showMessage('Could not load employees', true);
    }
}

// Show employees in table
function showEmployeesTable() {
    const tbody = document.getElementById('employeesList');

    if (employees.length === 0) {
        tbody.innerHTML = '<tr><td colspan="5" class="text-center">No employees found</td></tr>';
    } else {
        tbody.innerHTML = employees.map(emp => `
            <tr>
                <td>${emp.name}</td>
                <td>${emp.age}</td>
                <td>${emp.address}</td>
                <td>${emp.email}</td>
                <td>
                    <button class="btn btn-sm btn-info" title="View" onclick="viewEmployee(${emp.id})">
                        <i class="bi bi-eye"></i>
                    </button>
                    <button class="btn btn-sm btn-warning" title="Edit" onclick="editEmployee(${emp.id})">
                        <i class="bi bi-pencil-square"></i>
                    </button>
                    <button class="btn btn-sm btn-danger" title="Delete" onclick="askDelete(${emp.id})">
                        <i class="bi bi-trash"></i>
                    </button>
                </td>
            </tr>
        `).join('');
    }
    if ($.fn.DataTable.isDataTable('#employeesTable')) {
        $('#employeesTable').DataTable().destroy();
    }
    $('#employeesTable').DataTable({
        responsive: true,
        paging: true,
        searching: true,
        info: true,
        columnDefs: [
            { orderable: false, targets: 4 } 
        ]
    });
}



// Show form for new employee
function addEmployee() {
    editingId = null;
    document.getElementById('employeeForm').reset();
    document.getElementById('employeeModalLabel').textContent = 'Add Employee';
    employeeModal.show();
}

// Show form for editing employee
function editEmployee(id) {
    const employee = employees.find(emp => emp.id === id);
    if (!employee) return;

    editingId = id;
    document.getElementById('name').value = employee.name;
    document.getElementById('age').value = employee.age;
    document.getElementById('address').value = employee.address;
    document.getElementById('email').value = employee.email;
    document.getElementById('employeeModalLabel').textContent = 'Edit Employee';
    employeeModal.show();
}

// Save employee o edit
async function saveEmployee() {
    const name = document.getElementById('name').value;
    const ageInput = document.getElementById('age');
    const age = ageInput.value ? Number(ageInput.value) : null; 
    const address = document.getElementById('address').value;
    const email = document.getElementById('email').value;

    if (!name) {
        showMessage('Name is required', true);
        return;
    }

    const employeeData = { name, age, address, email };

    try {
        let response;
        if (editingId) {
            response = await fetch(`${API_BASE_URL}/employees/${editingId}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(employeeData)
            });
        } else {
            response = await fetch(`${API_BASE_URL}/employees`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(employeeData)
            });
        }

        if (response.ok) {
            employeeModal.hide();
            loadEmployees();
            showMessage(editingId ? 'Employee updated!' : 'Employee added!');
        } else {
            showMessage('Failed to save employee', true);
        }
    } catch (error) {
        showMessage('Error saving employee', true);
    }
}

function viewEmployee(id) {
    const emp = employees.find(e => e.id === id);
    if (!emp) return;

    const content = `
        <div class="card">
            <div class="card-body">
                <p><strong>Name:</strong> ${emp.name}</p>
                <p><strong>Age:</strong> ${emp.age || '-'}</p>
                <p><strong>Address:</strong> ${emp.address || '-'}</p>
                <p><strong>Email:</strong> ${emp.email || '-'}</p>
            </div>
        </div>
    `;

    document.getElementById('viewEmployeeContent').innerHTML = content;
    viewEmployeeModal.show();
}

function viewEmployeeListModal() {
    if (employees.length === 0) {
        Swal.fire('No employees found', '', 'info');
        return;
    }

    const content = `
        <div class="table-responsive">
            <table class="table table-bordered table-striped">
                <thead class="table-light">
                    <tr>
                        <th>Name</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Email</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    ${employees.map(emp => `
                        <tr>
                            <td>${emp.name}</td>
                            <td>${emp.age || '-'}</td>
                            <td>${emp.address || '-'}</td>
                            <td>${emp.email || '-'}</td>
                            <td>
                                <button class="btn btn-sm btn-info" onclick="viewEmployee(${emp.id})">View</button>
                            </td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>
        </div>
    `;
    document.getElementById('viewEmployeeContent').innerHTML = content;
    viewEmployeeModal.show();
}

function askDelete(id) {
    deleteId = id;
    deleteModal.show();
}

// delete
async function confirmDelete() {
    if (!deleteId) return;

    try {
        const response = await fetch(`${API_BASE_URL}/employees/${deleteId}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            deleteModal.hide();
            loadEmployees();
            showMessage('Employee deleted!');
        } else {
            showMessage('Failed to delete employee', true);
        }
    } catch (error) {
        showMessage('Error deleting employee', true);
    }
}
