'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections
Imports System.Linq
Imports DevExpress.Data.Utils
Namespace DevExpress.Web.Demos
    Public NotInheritable Class PatientsDataGenerator

        Private Sub New()
        End Sub

        Private Shared inMemoryData As IEnumerable
        Private Shared ReadOnly clinics() As String = { "St. Mary Hospital", "Florida General Hospital", "Johnson Neuropsychiatric Hospital", "Orlando Clinic", "Redmond Medical Center", "Dobson Institute for Rehabilitation", "Cofir Clinic", "St. Petersburg University Hospital", "University of Jenkintown Medical Center" }
        Private Shared ReadOnly firstNames() As String = { "Jacob", "Michael", "Joshua", "Matthew", "Daniel", "Christopher", "Andrew", "Ethan", "Joseph", "William", "Anthony", "David", "Alexander", "Nicholas", "Ryan", "Tyler", "James", "John", "Jonathan", "Noah", "Brandon", "Christian", "Dylan", "Samuel", "Benjamin", "Zachary", "Nathan", "Logan", "Justin", "Gabriel", "Jose", "Austin", "Kevin", "Elijah", "Caleb", "Robert", "Thomas", "Jordan", "Cameron", "Jack", "Hunter", "Jackson", "Angel", "Isaiah", "Evan", "Isaac", "Mason", "Luke", "Jason", "Gavin", "Jayden", "Aaron", "Connor", "Aiden", "Aidan", "Kyle", "Juan", "Charles", "Luis", "Adam", "Lucas", "Brian", "Eric", "Adrian", "Nathaniel", "Sean", "Alex", "Carlos", "Bryan", "Ian", "Owen", "Landon", "Julian", "Chase", "Cole", "Diego", "Jeremiah", "Steven", "Sebastian", "Xavier", "Timothy", "Carter", "Wyatt", "Brayden", "Blake", "Hayden", "Devin", "Cody", "Richard", "Seth", "Dominic", "Jaden", "Antonio", "Miguel", "Liam", "Patrick", "Carson", "Jesse", "Tristan" }
        Private Shared ReadOnly lastNames() As String = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King", "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter", "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins", "Stewart", "Sanchez", "Morris", "Rogers", "Reed", "Cook", "Morgan", "Bell", "Murphy", "Bailey", "Rivera", "Cooper", "Richardson", "Cox", "Howard", "Ward", "Torres", "Peterson", "Gray", "Ramirez", "James", "Watson", "Brooks", "Kelly", "Sanders", "Price", "Bennett", "Wood", "Barnes", "Ross", "Henderson", "Coleman", "Jenkins", "Perry", "Powell", "Long", "Patterson", "Hughes", "Flores", "Washington", "Butler", "Simmons", "Foster", "Gonzales", "Bryant", "Alexander", "Russell", "Griffin", "Diaz", "Hayes" }
        Private Shared ReadOnly doctorNames() As String = { "Peter Dolan", "Ryan Fischer", "Richard Fisher", "Tom Hamlett", "Mark Hamilton", "Steve Lee", "Jimmy Lewis", "Jeffrey W McClain", "Andrew Miller", "Dave Murrel", "Bert Parkins", "Mike Roller", "Ray Shipman", "Paul Bailey", "Brad Barnes", "Carl Lucas", "Jerry Campbell" }
        Public Shared Function GetInMemoryData() As IEnumerable
            If inMemoryData Is Nothing Then
                inMemoryData = GenerateInMemory(3000)
            End If
            Return inMemoryData
        End Function
        Private Shared Function GenerateInMemory(ByVal count As Integer) As IList
            Dim firstNameField = New DatabaseGenerator.FieldData("FirstName", firstNames)
            Dim lastNameField = New DatabaseGenerator.FieldData("LastName", lastNames)
            Dim doctorField = New DatabaseGenerator.FieldData("Doctor", doctorNames)
            Dim clinicField = New DatabaseGenerator.FieldData("Clinic", clinics)
            Dim visitDateField = New DatabaseGenerator.FieldData("VisitDate", AddressOf GenerateVisitDate)
            Dim durationField = New DatabaseGenerator.FieldData("Duration", AddressOf GenerateDuration)
            Return Enumerable.Range(0, count).Select(Function(i) New With {Key .ID = i, Key .Patient = String.Format("{0} {1}", firstNameField.GenerateValue().ToString(), lastNameField.GenerateValue().ToString()), Key .Doctor = doctorField.GenerateValue().ToString(), Key .Clinic = clinicField.GenerateValue().ToString(), Key .VisitDate = Convert.ToDateTime(visitDateField.GenerateValue()), Key .Duration = Convert.ToInt32(durationField.GenerateValue())}).ToList()
        End Function
        Private Shared Function GenerateDuration(ByVal rnd As NonCryptographicRandom) As Object
            Return rnd.Next(20, 120)
        End Function
        Private Shared Function GenerateVisitDate(ByVal rnd As NonCryptographicRandom) As Object
            Return Date.Now.Date.Subtract(TimeSpan.FromDays(rnd.Next(-365, 365)))
        End Function
    End Class
End Namespace
