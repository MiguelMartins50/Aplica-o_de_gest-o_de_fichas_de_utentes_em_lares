import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground } from 'react-native';
import axios from 'axios';

export default function UtenteFamiliar({ navigation,route  }) {
  const { params } = route;
  const {FamiliarData, FamiliarID} = route.params || {};
  const handleLogin = () => {
    console.log(FamiliarData);
    console.log(FamiliarID);
    axios.get(`http://192.168.1.80:8800/utente_familiar?Familiar_idFamiliar=${FamiliarData.idFamiliar}`).then(UFResponse => {
        if (UFResponse.data.length > 0) {
          const UF = UFResponse.data;
          console.log('UF:'+UF);
          console.log('UFID:'+UF.Utente_idUtente)
          axios.get(`http://192.168.1.80:8800/utente?idUtente=${UF.Utente_idUtente}`).then(UResponse => {
            const U = UResponse.data;
              console.log('U:'+JSON.stringify(U));
            if (UResponse.data.length > 0) {
              const U = UResponse.data;
              console.log('U:'+JSON.stringify(U));
            }
          })
        }
      })
      
      
      }
      useEffect(()=>{
        handleLogin();
      })
  return (
      <View style={styles.container}>
        
        <View style={styles.Text2}><Text tyle={styles.ButtonText}>Familiares</Text></View>
        
        
        
        <StatusBar style="auto" />

        <View style={styles.Imag}>
          <Image source={require('../Image/Image2.png')} style={styles.Image2} />
        </View>
      </View>
   
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
  
  },
  Button: {
    backgroundColor: '#3498db',
    paddingVertical: 10,
    paddingHorizontal: 20, 
    borderRadius: 8,
    height: 40,
    width: 300,  
    marginBottom: 20,
    justifyContent: 'center',  
    alignItems: 'center',    
  },
  Text2: {
    paddingVertical: 200,
    paddingHorizontal: 20, 
    borderRadius: 8, 

    justifyContent: 'center',  
    alignItems: 'center',    
  },
  ButtonText: {
    color: '#fff',
    fontSize: 17,
    
  },
  Image1:{
    height:110,
    width:400,
    marginTop:-175
    
    
  },
  Image2: {
    height:205,
    width:410,
    padding:20,
    marginBottom:-150
   
  },
  Imag:{
    padding:50
  },
  Imag1:{
    padding:195,
  },
  sairButton: {
    backgroundColor: 'white',
    padding: 2,
    width:60,
    borderRadius: 5,
    alignSelf: 'flex-end', 
    margin: 40,
  },
  sairButtonText: {
    color: 'black',
    fontWeight: 'bold',
    textAlign:'center'
  },
  ButtonText: {
    color: '#fff',
    fontSize: 17,
    
  },
});