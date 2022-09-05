#include <stdio.h>
#include <unistd.h>
#include <stdint.h>
#include <stdlib.h>
#define SIZE 1000

int xor_arr(int *a, int *b){
    for (int i = 0; i < 4; i++){
        if ((a[i] ^ b[i]) != 0){
            return 1;
        }
    }
    return 0;
}


void solve(int** net, int** ip, int *mask){
    int visited[SIZE] = {0};
    for (int i = 0; i < SIZE; i++){
        if(visited[i] == 0){
            for (int j = 0; j < SIZE; j++){
                if (i != j){
                    if (xor_arr(ip[i], ip[j]) == 0){
                        visited[j] = 1;
                    }
                }    
            }
        }
    }

    for (int i = 0; i < SIZE; i++){
        for (int j = 0; j < 4; j++){
            net[i][j] = ip[i][j] & mask[j];
        }
    }
    int max_hosts = 1;
    int hosts;
    int networks = 0;

    for (int i = 0; i < SIZE; i++){
        if (visited[i] == 0){
            networks++;
            visited[i] = 1;
            hosts = 1;
            for (int j = 0; j < SIZE; j++){
                if (visited[j] == 0){
                    if (xor_arr(net[i],net[j]) == 0){
                        visited[j] = 1;
                        hosts++;
                    } 
                }
            }

            if (hosts > max_hosts){
                max_hosts = hosts;
            }
        } 
    }
    printf("%d %d", networks, max_hosts);
}

int main() {
    int* mask;
    int** ip;
    int** net;
    mask = (int*) malloc(sizeof(int)*4);
    ip = (int**) malloc(sizeof(int*)*SIZE);
    for (int i = 0; i < SIZE; i++){
        ip[i] = (int*) malloc(sizeof(int)*4);
    }
    net = (int**) malloc(sizeof(int*)*SIZE);
    for (int i = 0; i < SIZE; i++){
        net[i] = (int*) malloc(sizeof(int)*4);
    }
    
    FILE *fp;    
    fp = fopen("./log1.txt", "r");

    for (int j = 0; j < 4; j++){
        fscanf(fp, "%d", &mask[j]);
    }
    for (int i = 0; i < SIZE ; i++){
        for (int j = 0; j < 4; j++){
            fscanf(fp, "%d", &ip[i][j]);
        }
    }
    fclose(fp);
    solve(net, ip, mask);
    return 0;
}