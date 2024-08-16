#include <bits/stdc++.h>
using namespace std;

class FileHandiling
{
    public:
    vector<string> ReadFileLines (string path) {
        vector<string> lines;
        fstream file_handler(path.c_str());
        if (file_handler.fail()) {
            cout << "\n\nERROR: Can't open the file\n\n";
            return lines;
        }
        string line;

        while (getline(file_handler, line)) {
            if (line.size() == 0)
                continue;
            lines.push_back(line);
        }

        file_handler.close();
        return lines;
    }

    vector<string> SpliteFileLine(string line,char separator=' '){
        string data = "";
        vector <string> Question_Info;
        for(auto c:line){
            if(c == separator){
                Question_Info.push_back(data);
                data = "";
            }
            else{
                data+=c;
            }
        }
        Question_Info.push_back(data);
        return Question_Info;
    }

    void WriteFileLines(string path, vector<string> lines, bool append = true) {
        auto status = ios::in | ios::out | ios::app;

        if (!append)
            status = ios::in | ios::out | ios::trunc;	// overwrite

        fstream file_handler(path.c_str(), status);

        if (file_handler.fail()) {
            cout << "\n\nERROR: Can't open the file\n\n";
            return;
        }
        for (auto line : lines)
            file_handler << line << "\n";

        file_handler.close();
    }

    void RecordNewLine(string line , string path){
        ofstream file_handler(path.c_str() , ios::app | ios::out);

        if (file_handler.fail()) {
            cout << "\n\nERROR: Can't open the file\n\n";
            return;
        }

        file_handler<<line<<"\n";
        file_handler.close();
    }

};

class Quetion
{
    private:
    FileHandiling file_haniling;
    string question_text;
    string answer;
    string id;
    string to_user_id;  // id for the user who receive the question.
    string from_user_id;  // id for the user who send the question.

    public:
    Quetion()
    {

    }

    Quetion(string line):answer("")
    {
        vector <string>Question_Info = file_haniling.SpliteFileLine(line, ',');
        question_text = Question_Info[0];
        id = Question_Info[1];
        from_user_id = Question_Info[2];
        to_user_id = Question_Info[3];
        answer = Question_Info[4];
    }

    const string& GetQuestionText() const{
        return question_text;
    }

    void SetQuestionText(string question_text_){
        this->question_text = question_text_;
    }
    
    const string& GetAnswer() const{
        return answer;
    }

    void SetAnswer(string answer_){
        this->answer = answer_;
    }
    
    const string& GetQuestionId() const{
        return id;
    }

    void SetQuestionId(string id_){
        this->id = id_;
    }

    const string& GetQuestionto_user_id() const{
        return to_user_id;
    }

    void SetQuestionto_user_id(string to_user_id_){
        this->to_user_id = to_user_id_;
    }

    const string& GetQuestionfrom_user_id() const{
        return from_user_id;
    }

    void SetQuestionfrom_user_id(string from_user_id_){
        this->from_user_id = from_user_id_;
    }
};

class User
{
    private:
    FileHandiling file_haniling;
    string username;
    string password;
    string id;
    string email;
    vector <Quetion> question_to_current_user;
    vector <Quetion> question_from_current_user;

    public:
    User()
    {

    }

    User(string line)
    {
        vector <string> substrs = file_haniling.SpliteFileLine(line); // To splite Line from txt file.
        username = substrs[0];
        password = substrs[1];
        email = substrs[2];
        id = substrs[3];
    }

    const string& GetUserName() const{
        return username;
    }

    void SetUserName(string username_){
        this->username = username_;
    }

    const string& GetPassword() const{
        return password;
    }

    void SetPassword(string pass_){
        this->password = pass_;
    }
    
    const string& GetUserId() const{
        return id;
    }

    void SetUserId(string id_){
        this->id = id_;
    }
    
    const string& GetEmail() const{
        return email;
    }

    void SetEmail(string email_){
        this->email = email_;
    }

    vector<Quetion>& Getquestion_to_current_user(){
        return question_to_current_user;
    }

    vector<Quetion>& Getquestion_from_current_user(){
        return question_from_current_user;
    }
};

class QuetionsManager
{
    private:
    vector <Quetion> questions_freq;
    FileHandiling file_haniling;

    public:

    QuetionsManager()
    {

    }

    void LoadQuestionDataBase(){
        vector <string> lines = file_haniling.ReadFileLines("questions.txt");
        questions_freq.clear();

        for(auto &line:lines){
            Quetion q(line);
            questions_freq.push_back(q);
        }
    }

    void LoadQuestionToFromUser(User& user) const{
        vector <Quetion>& q_to_me = user.Getquestion_to_current_user();
        vector <Quetion>& q_from_me = user.Getquestion_from_current_user();
        q_to_me.clear();
        q_from_me.clear();

        for(auto& question:questions_freq){
            if(question.GetQuestionfrom_user_id() == user.GetUserId()){
                q_from_me.push_back(question);
            }
            else if(question.GetQuestionto_user_id() == user.GetUserId()){
                q_to_me.push_back(question);
            }
        }
    }

    string GenerateQuestionId() const{
        int id = 1;
        id+=questions_freq.size();
        return to_string(id);
    }

    void PrintQuestionTome(User& user) const{
        if(user.Getquestion_to_current_user().size() == 0){
            cout<<"\nThere Is No Questions.";
            return;
        }

        for(auto&q:user.Getquestion_to_current_user()){
            cout<<"\nQuestion id ("<<q.GetQuestionId()<<") from user id ("<<q.GetQuestionfrom_user_id()<<"): "<<q.GetQuestionText()<<"\n";
            if(q.GetAnswer() == ""){
                cout<<"\tNot Answered Yet\n";
            }
            else{
                cout<<"\t"<<q.GetAnswer()<<"\n";
            }
        }
    }

    void PrintQuestionFromme(User& user) const{
        if(user.Getquestion_from_current_user().size() == 0){
            cout<<"\nThere Is No Questions.";
            return;
        }

        for(auto&q:user.Getquestion_from_current_user()){
            cout<<"\nQuestion id ("<<q.GetQuestionId()<<") to user id ("<<q.GetQuestionto_user_id()<<"): "<<q.GetQuestionText()<<"\n";
            if(q.GetAnswer() == ""){
                cout<<"\tNot Answered Yet\n";
            }
            else{
                cout<<"\t"<<q.GetAnswer()<<"\n";
            }
        }
    }

    void AskQuestion(const User& user){
        string to_id,question_text,question_id;
        cout<<"\nEnter user id or -1 to cancel: ";
        cin>>to_id;

        if(to_id == "-1"){
            return;
        }

        // you shoud varifay the user id exist.
        cout<<"Enter question text or -1 to cancel: ";
        cin.ignore();
        getline(cin , question_text);

        if(question_text == "-1"){
            return;
        }

        question_id = GenerateQuestionId();

        ostringstream oss;
		oss<<question_text<<","<<question_id<<","<<user.GetUserId()<<","<<to_id<<",";
        file_haniling.RecordNewLine(oss.str() , "questions.txt");
    }

    bool IsYouCanAnswer(User& user , string id) const{
        for(auto& q:user.Getquestion_to_current_user()){
            if(q.GetQuestionId() == id){
                return true;
            }
        }
        return false;
    }

    void UpdateDataBase(){
        vector<string> lines;
        for(auto& line:questions_freq){
            ostringstream oss;
            oss<<line.GetQuestionText()<<","<<line.GetQuestionId()<<","<<line.GetQuestionfrom_user_id()<<","<<line.GetQuestionto_user_id()<<","<<line.GetAnswer();
            lines.push_back(oss.str());
        }
        file_haniling.WriteFileLines("questions.txt" , lines , false);
    }

    void AnswerQuestion(User& user){
        string q_id,ans_text;
        cout<<"\nEnter Question id or -1 to cancel: ";
        cin>>q_id;

        if(q_id == "-1"){
            return;
        }

        while(!IsYouCanAnswer(user , q_id))
        {
            cout<<"Error: Invalid Question Id";
            cout<<"\n\nEnter Question id or -1 to cancel: ";
            cin>>q_id;

            if(q_id == "-1"){
                return;
            }
        }

        for(auto& q:user.Getquestion_to_current_user()){
            if(q_id == q.GetQuestionId() && q.GetAnswer() != ""){
                cout<<"Warning The Question Is Already Answered.\n";
            }
        }

        cout<<"\nEnter Your Answer or -1 to cancel: ";
        cin.ignore();
        getline(cin , ans_text);

        if(ans_text == "-1"){
            return;
        }

        string answer = "Answer: ";answer+=ans_text;
        for(auto& q:questions_freq){
            if(q_id == q.GetQuestionId()){
                q.SetAnswer(answer);
            }
        }
        UpdateDataBase();
    }

    bool IsYouCanDeletQuestion(User& user , string id) const{
        for(auto& q:user.Getquestion_from_current_user()){
            if(q.GetQuestionId() == id){
                return true;
            }
        }
        return false;
    }

    void DeleteQuestion(User& user){
        string Q_id;
        cout<<"Enter Question id or -1 to cancel: ";
        cin>>Q_id;

        if(Q_id == "-1"){
            return;
        }

        while(!IsYouCanDeletQuestion(user , Q_id))
        {
            cout<<"Error: Invalid Question Id";
            cout<<"\n\nEnter Question id or -1 to cancel: ";
            cin>>Q_id;

            if(Q_id == "-1"){
                return;
            }
        }

        int pos=0;
        for(auto& q:questions_freq){
            if(Q_id == q.GetQuestionId()){
               questions_freq.erase(questions_freq.begin()+pos); 
            }
            pos++;
        }

        UpdateDataBase();
    }

    void PrintOneQuestion(const Quetion& q) const{
        cout<<"\nQuestion id ("<<q.GetQuestionId()<<") from user id ("<<q.GetQuestionfrom_user_id()<<") to user id ("<<q.GetQuestionto_user_id()<<"): "<<q.GetQuestionText()<<"\n";
        cout<<"\t"<<q.GetAnswer()<<"\n";
    }

    void GetFeed(){
        const vector<string>& Questions = file_haniling.ReadFileLines("questions.txt");
        for(auto &q:Questions){
            Quetion Q(q);
            if(Q.GetAnswer() != ""){
                PrintOneQuestion(Q);
            }
        }
    }
};

class UsersManager
{
    private:
    User Current_user;
    vector <User> Users_names;
    FileHandiling file_haniling;

    public:
    UsersManager()
    {

    }

    void LoadUsersDataBase(){
        vector <string> lines = file_haniling.ReadFileLines("Users.txt");
        Users_names.clear();

        for(auto &line:lines){
            User user(line);
            Users_names.push_back(user);
        }
    }

    void ListSystemUsers() const{
        cout<<"\nName\tId\n";
        for(auto& user:Users_names){
            cout<<user.GetUserName()<<"\t"<<user.GetUserId()<<"\n";
        }
    }
    
    bool IsUserExist(string user_name ,string pass) const{
        for(auto& user:Users_names){
            if(user.GetUserName() == user_name && user.GetPassword() == pass){
                return true;
            }
        }
        return false;
    }

    void RecordNewUser(User &new_user){
        ostringstream oss;
        oss<<new_user.GetUserName()<<" "<<new_user.GetPassword()<<" "<<new_user.GetEmail()<<" "<<new_user.GetUserId();
        
        file_haniling.RecordNewLine(oss.str() , "Users.txt");
    }

    string GenerateNewUserId() const{
        int id = 100;
        id+=Users_names.size();
        return to_string(id);
    }

    string GetUserId(string user_name) const{
        for(auto& user:Users_names){
            if(user.GetUserName() == user_name){
                return user.GetUserId();
            }
        }
        return "";
    }

    void Sinup(){
        string user_name,pass,email;
        cout<<"\nEnter The Username (No Spaces): ";
        cin>>user_name;
        Current_user.SetUserName(user_name);
        cout<<"\nEnter The Password: ";
        cin>>pass;
        Current_user.SetPassword(pass);
        cout<<"\nEnter The Email: ";
        cin>>email;
        Current_user.SetEmail(email);

        while(IsUserExist(Current_user.GetUserName() , Current_user.GetPassword()))
        {
            cout<<"Already used. Try again..\n";
            cout<<"\nEnter The Username (No Spaces): ";
            cin>>user_name;
            Current_user.SetUserName(user_name);
            cout<<"\nEnter The Password: ";
            cin>>pass;
            Current_user.SetPassword(pass);
        }

        Current_user.SetUserId( GenerateNewUserId());
        Users_names.push_back(Current_user);
        RecordNewUser(Current_user);
    }

    void Login(){
        string user_name,pass;
        cout << "Enter user name & password: ";
        cin>>user_name>>pass;
        Current_user.SetUserName(user_name);
        Current_user.SetPassword(pass);

        while(!IsUserExist(Current_user.GetUserName() , Current_user.GetPassword()))
        {
            cout<<"Invalid user name or password. Try again..\n";
		    cout << "\nEnter user name & password: ";
            cin>>user_name>>pass;
            Current_user.SetUserName(user_name);
            Current_user.SetPassword(pass);
        }
        Current_user.SetUserId(GetUserId(Current_user.GetUserName()));
    }

    string RegiserationMenue() const{
        cout<<"\n\n\t\t..........................\n";
        cout<<"\t\t|WELCOME IN ASK ME SYSTEM|\n";
        cout<<"\t\t..........................\n\n";

        cout<<"Menue:\n";
        cout<<"\t1: Login\n";
        cout<<"\t2: Sing Up\n";

        string choice;
        cout<<"\nEnter a Number In Range 1 - 2 : ";
        cin>>choice;

        while (choice != "1" && choice != "2")
        {
            cout<<"\nPlease Enter a Number In Range 1 - 2 : ";
            cin>>choice;
        }
        return choice;
    }

    User& GetCurrentUser(){
        return Current_user;
    }
};

class System
{
    private:
    UsersManager users_manager;
    QuetionsManager question_manager;

    int menu(){
        cout<<"\n\n\t\t....................\n";
        cout<<"\t\t<< ASK ME SYSTEM >>\n";
        cout<<"\t\t....................\n\n";

        cout<<"\nMenue:\n";

        cout<<"\t1: Print Question To Me.\n";
        cout<<"\t2: Print Question From Me.\n";
        cout<<"\t3: Answer Question.\n";
        cout<<"\t4: Delete Question.\n";
        cout<<"\t5: Ask Question.\n";
        cout<<"\t6: List System Users.\n";
        cout<<"\t7: Feed.\n";
        cout<<"\t8: Logout.\n";

        cout<<"\nEnter a Number In Range 1 - 8 : ";

        int selection;
        cin>>selection;

        while (selection < 1 || selection > 8)
        {
            cout<<"\nPlease Enter a Number In Range 1 - 8 : ";
            cin>>selection;
        }
 
        return selection;
    }

    bool IsFileExist(string path){
        fstream file_handler;
        file_handler.open(path.c_str());

        if(file_handler){
            return true;
        }

        return false;
    }

    public:
    void run(){
        //system("cls");

        if(IsFileExist("Users.txt")){
            users_manager.LoadUsersDataBase();
        }

        if(IsFileExist("questions.txt")){
            question_manager.LoadQuestionDataBase();
        }

        string choice = users_manager.RegiserationMenue();

        if(choice == "1")
            users_manager.Login();
        
        else
            users_manager.Sinup();

        //system("cls");
        while(true)
        {
            int selection = menu();

            if(IsFileExist("questions.txt")){
                question_manager.LoadQuestionDataBase();
            }

            question_manager.LoadQuestionToFromUser(users_manager.GetCurrentUser());

            if(selection == 1)
                question_manager.PrintQuestionTome(users_manager.GetCurrentUser());
            
            else if (selection == 2)
                question_manager.PrintQuestionFromme(users_manager.GetCurrentUser());
                
            else if (selection == 3)
                question_manager.AnswerQuestion(users_manager.GetCurrentUser());
    
            else if (selection == 4)
                question_manager.DeleteQuestion(users_manager.GetCurrentUser());
                
            else if (selection == 5)
                question_manager.AskQuestion(users_manager.GetCurrentUser());
            
            else if(selection == 6)
                users_manager.ListSystemUsers();
            
            else if(selection == 7)
                question_manager.GetFeed();
            
            else if(selection == 8)
                break;  // loge out
        }

    run();

    }
};

int main(){
    System use;
    use.run();

}


