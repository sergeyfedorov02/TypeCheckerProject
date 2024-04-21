language core;
extend with #references;


fn main(n : Nat) -> Nat {
	return (fn (x : Nat) { return *n }) (0)
}
