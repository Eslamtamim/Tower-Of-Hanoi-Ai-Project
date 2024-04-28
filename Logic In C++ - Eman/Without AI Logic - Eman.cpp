#include<bits/stdc++.h>
using namespace std;

int click_state;
int N_disks;
pair<int,int> last_click;
stack<pair<int,int>> logs;
vector<stack<int>> board(3);

void intialize(int n = 5){
    board.assign(3, stack<int>());
    N_disks = n;
    for (int i = N_disks; i > 0; --i) {
        board[0].push(i);
    }
    click_state = 2;
    while(!logs.empty()) logs.pop();
}

void transfer(pair<int,int> p){
    int temp = board[p.first].top();
    board[p.first].pop();
    board[p.second].push(temp);
}

void go_back(){
    if(!logs.empty()){
        //swap pair
        int temp = logs.top().first;
        logs.top().first = logs.top().second;
        logs.top().second = temp;
        //transfer
        transfer(logs.top());
        logs.pop();
    }


}

void click(int index){
    click_state = 3 - click_state;
    if(click_state == 1) {
        if(board[index].empty()) click_state = 3-click_state; //no need to do anything
        else last_click.first = index;
    }
    else{
        if(!board[index].empty() && board[index].top() < board[last_click.first].top()){
            cout<<"unvalid move";
            //the next click will be distination click;
            click_state = 3- click_state;
        }
        else{
            last_click.second = index;
            transfer(last_click);
            logs.push(last_click);
        }
    }
}

signed main(){

    intialize(3);
    click(0);
    click(2);

    click(0);
    click(2);
    click(1);

    click(2);
    click(1);

    click(0);
    click(2);

    click(1);
    click(0);

    click(1);
    click(2);

    click(0);
    click(2);

    click(1);
    go_back();
    click(0);
    click(2);


    return 0;
}