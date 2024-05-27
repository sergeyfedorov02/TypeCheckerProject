language core;

extend with #universal-types, #type-cast;

fn identity(x : Nat) -> Bool {
	return x cast as Bool
}

fn main(x : Nat) -> Bool {
	return identity[Nat](x)
}