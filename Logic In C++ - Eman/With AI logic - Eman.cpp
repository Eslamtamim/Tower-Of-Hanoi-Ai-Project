#include<bits/stdc++.h>
using namespace std;

int N_disks;
vector<stack<int>> board(3);

void intialize(int n = 5){
    N_disks = n;
    board.assign(3, stack<int>());
    for (int i = n; i > 0; --i) {
        board[0].push(i);
    }
}

void transfer(pair<int,int> p, vector<stack<int>> &board){
    int temp = board[p.first].top();
    board[p.first].pop();
    board[p.second].push(temp);
}



class Node{
private:
    vector<stack<int>> state;
    Node *parent {nullptr};
    vector<Node *> successors;
    pair<int,int> move{NULL,NULL}; //to reach

    int g_cost{};
    int h_cost{};
public:

    Node(vector<stack<int>> state, Node *parent, pair<int,int> to_reach_move){
        this->state = state;
        this->parent = parent;
        this-> move = to_reach_move;
    }

    vector<stack<int>> get_state(){return state;}
    vector<Node *> get_successors(){return successors;}
    pair<int,int> get_move(){return move;}
    Node* get_parent(){return parent;}
    void set_gCost(int gCost){g_cost = gCost;}
    void set_hCost(int hCost){h_cost = hCost;}
    int get_gCost(){return g_cost;}
    int get_hCost(){return h_cost;}

    void generate_successors(){
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                if(i != j){
                    if(move.first == j && move.second == i) continue;
                    if(!state[i].empty() && (state[j].empty() || (state[j].top() > state[i].top()))){
                        vector<stack<int>> newState (state.begin(),state.end());
                        transfer({i,j}, newState);
                        Node * newNode = new Node(newState, this, {i,j});
                        successors.push_back(newNode);
                    }
                }
            }
        }
    }

    bool is_ans(){
        return (state[1].size() == N_disks || state[1].size() == N_disks);
    }

    stack<pair<int,int>> generate_path(){
        auto curr = this;
        stack<pair<int,int>> path;
        while(curr->parent != nullptr){
            path.push(curr->move);
            curr = curr->parent;
        }
        return path;
    }

};

int heuristic_func(vector<stack<int>> state) {
    return 0;
}


stack<pair<int,int>> AStar_Search(Node *root) {

    priority_queue<pair<int, Node*>, vector<pair<int, Node*>>, greater<>> frontier;
    int h_cost = heuristic_func(root->get_state());
    root->set_hCost(h_cost);
    frontier.push({root->get_hCost(), root});
    set<vector<stack<int>>> visited_states;
    visited_states.insert(root->get_state());

    while (!frontier.empty()) {
        auto current_node = frontier.top();
        frontier.pop();

        if (current_node.second->is_ans()) {
            return current_node.second->generate_path();
        }
        current_node.second->generate_successors();

        for (auto successor : current_node.second->get_successors()) {
            int g_cost = current_node.second->get_gCost()+1;
            int h_cost = heuristic_func(successor->get_state()) + g_cost;

            successor->set_gCost(g_cost);
            successor->set_hCost(h_cost);

            if (visited_states.find(successor->get_state()) == visited_states.end()) {
                frontier.push({h_cost, successor});
                visited_states.insert(successor->get_state());
            }
        }
    }

    return stack<pair<int,int>>();
}


signed main(){

    intialize();
    Node* root = new Node(board,nullptr,{NULL,NULL});
    auto solution_path = AStar_Search(root);
    int cost = solution_path.size();

    while(!solution_path.empty()){
        auto temp = solution_path.top();
        cout<<temp.first<<" "<<temp.second<<endl;
        solution_path.pop();
    }
    cout<<cost<<endl;

    return 0;
}