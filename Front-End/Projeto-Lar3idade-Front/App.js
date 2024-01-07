import React, { useState } from 'react';
import { StatusBar } from 'expo-status-bar';
import 'react-native-gesture-handler';
import { StyleSheet,Image,Text, View, TextInput, TouchableOpacity, ImageBackground } from 'react-native';
import { createDrawerNavigator } from '@react-navigation/drawer';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { NavigationContainer} from '@react-navigation/native';
import UtenteScreen from './Telas/Utente.js';
import FamiliarScreen from './Telas/Familiar.js';
import Login from './Telas/Login.js'

const Stack = createNativeStackNavigator();
const Drawer = createDrawerNavigator();


export const UtenteDrawer = () => (
  <Drawer.Navigator >
    <Drawer.Screen name="UtenteScreen" component={UtenteScreen} />
  </Drawer.Navigator>
);

const FamiliarDrawer = () => (
  <Drawer.Navigator>
    <Drawer.Screen name="FamiliarScreen" component={FamiliarScreen} />
  </Drawer.Navigator>
);

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Login" screenOptions={{headerShown: false}} >
        <Stack.Screen name="Login" component={Login} />
        <Stack.Screen name="UtenteDrawer" component={UtenteDrawer} />
        <Stack.Screen name="FamiliarDrawer" component={FamiliarDrawer} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
    marginBottom:-440
  },
  login: {
    fontSize: 24,
    marginBottom: 24,
    textAlign:'center'
  },
  inputContainer: {
    width: '100%',
    marginBottom: 16,
  },
  input: {
    height: 40,
    borderColor: 'gray',
    borderWidth: 1,
    borderRadius: 8,
    paddingHorizontal: 16,
    marginBottom: 16,
    
  },
  loginButton: {
    backgroundColor: '#3498db',
    paddingVertical: 9,
    paddingHorizontal: 150,
    borderRadius: 8,
    height: 40,
    marginBottom:35
  },
  loginButtonText: {
    color: '#fff',
    fontSize: 17,
    
    
  },
  logo:{
    width: 100,  
    height: 100, 
    resizeMode: 'contain',
    marginBottom: 20,
    alignItems: 'center'
  },
  Image2:{
    padding:107,
    width:440,
  },
  image3:{
    width:400,
    height:350,
    marginTop:-400
  },
  text: {
    color: 'black',
    fontSize: 30,
    lineHeight: 60,
    textAlign: 'center',
    marginTop:110
  },

  
});

